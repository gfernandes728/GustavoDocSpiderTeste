using GustavoDocSpiderTeste.Business.Documents;
using GustavoDocSpiderTeste.Data.Data;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GustavoDocSpiderTeste.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly DocumentsBusiness _documentBusiness;

        public DocumentsController(ApplicationDbContext context)
            => _documentBusiness = new DocumentsBusiness(context);

        #region - Public methods -

        public async Task<IActionResult> Index(string search, 
            int pageNumber = 1, 
            int pageSize = 10, 
            string orderBy = "CreatedAt desc")
        {
            var result = await _documentBusiness.GetListDocuments(search, pageNumber, pageSize, orderBy);

            ViewBag.Search = search;
            ViewBag.OrderBy = orderBy;

            return View(result);
        }

        public async Task<IActionResult> Details(int? id)
            => await GetDocuments(id);

        public IActionResult Create()
            => View(new DocumentViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel vm)
        {
            if (await _documentBusiness.HasDocumentTitle(vm.Title))
                ModelState.AddModelError("Title", "Já existe um documento com esse título.");

            if (vm.File == null || vm.File.Length == 0)
                ModelState.AddModelError("File", "Arquivo é obrigatório.");
            else if (_documentBusiness.IsForbiddenExtensionsFile(vm.File.FileName))
                ModelState.AddModelError("File", "Tipo de arquivo não permitido.");

            if (!ModelState.IsValid || !(await _documentBusiness.CreateDocument(vm)))
                return View(vm);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
            => await GetDocuments(id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DocumentViewModel vm)
        {
            if (id != vm.Id)
            {
                ModelState.AddModelError("Title", "Documento para edição invãlido.");
                return View(vm);
            }

            if (await _documentBusiness.HasDocumentTitleForEdit(vm.Title, vm.Id))
                ModelState.AddModelError("Title", "Já existe um documento com esse título.");

            if (vm.File != null && _documentBusiness.IsForbiddenExtensionsFile(vm.File.FileName))
                ModelState.AddModelError("File", "Tipo de arquivo não permitido.");

            if (!ModelState.IsValid)
                return View(vm);

            var result = await _documentBusiness.EditDocument(id, vm);
            if (result == null || result == false)
            {
                ModelState.AddModelError("Title", "Falha ao editar documento.");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
            => await GetDocuments(id);

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _documentBusiness.DeleteDocument(id);
            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download(int id)
        {
            var result = await _documentBusiness.GetDocumentForDownload(id);
            if (result == null) 
                return NotFound();

            return File(result.FileData, "application/octet-stream", result.FileName);
        }

        #endregion

        #region - Auxiliar methods -

        public async Task<IActionResult> GetDocuments(int? id)
        {
            var result = await _documentBusiness.GetDocument(id);
            if (result == null) 
                return NotFound();

            return View(result);
        }

        #endregion
    }
}
