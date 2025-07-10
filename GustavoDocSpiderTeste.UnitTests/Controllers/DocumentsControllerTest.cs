using AutoBogus;
using GustavoDocSpiderTeste.Controllers;
using GustavoDocSpiderTeste.Data.Data;
using GustavoDocSpiderTeste.Models.Models;
using GustavoDocSpiderTeste.UnitTests.Mock;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GustavoDocSpiderTeste.UnitTests.Controllers
{
    public class DocumentsControllerTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly ApplicationDbContext _context;
        private readonly DocumentsController _documentsController;

        private readonly DocumentViewModelMock _documentViewModelMock;

        private readonly DocumentModel _documentModel;
        private readonly DocumentModel _documentSecondModel;

        public DocumentsControllerTest()
        {
            _autoFaker = AutoFaker.Create();
            _documentViewModelMock = new DocumentViewModelMock();

            _context = ApplicationDbContextMock.Create();
            _documentsController = new DocumentsController(_context);

            var documentModelMock = new DocumentModelMock();
            _documentModel = documentModelMock.Create();
            _documentSecondModel = documentModelMock.Create();

            SaveDocumentModel().Wait();
        }

        [Fact]
        public async Task Index_Test()
            => Assert.NotNull(await _documentsController.Index(""));

        [Fact]
        public async Task Details_Test()
            => Assert.NotNull(await _documentsController.Details(_documentModel.Id));

        [Fact]
        public void CreateGet_Test()
            => Assert.NotNull(_documentsController.Create());

        #region - Test : Create (Post) -

        [Fact]
        public async Task CreatePost_TitleError_Test()
        {
            var documentViewModel = GetDocumentViewModel();
            documentViewModel.Title = _documentModel.Title;

            Assert.NotNull(await Create(documentViewModel));
        }

        [Fact]
        public async Task CreatePost_FileNullError_Test()
        {
            var documentViewModel = GetDocumentViewModel();
            documentViewModel.File = null;

            Assert.NotNull(await Create(documentViewModel));
        }

        [Fact]
        public async Task CreatePost_ForbiddenExtensionsFileError_Test()
        {
            var documentViewModel = _documentViewModelMock.CreateWithForbiddenFileExtensions();
            Assert.NotNull(await Create(documentViewModel));
        }

        [Fact]
        public async Task CreatePost_Test()
        {
            var documentViewModel = GetDocumentViewModel();
            Assert.NotNull(await Create(documentViewModel));
        }

        #endregion

        [Fact]
        public async Task EditGet_Test()
            => Assert.NotNull(await _documentsController.Edit(_documentModel.Id));

        #region - Test : Edit (Post) -

        [Fact]
        public async Task EditPost_DifferentIds_Test()
        {
            var documentViewModel = GetDocumentViewModel();
            documentViewModel.Id = GenerateId();

            Assert.NotNull(await Edit(_documentModel.Id, documentViewModel));
        }

        [Fact]
        public async Task EditPost_TitleError_Test()
        {
            var documentViewModel = GetDocumentViewModel();
            documentViewModel.Id = _documentModel.Id;
            documentViewModel.Title = _documentSecondModel.Title;

            Assert.NotNull(await Edit(_documentModel.Id, documentViewModel));
        }

        [Fact]
        public async Task EditPost_FileNull_Test()
        {
            var documentViewModel = GetDocumentViewModel();
            documentViewModel.Id = _documentModel.Id;
            documentViewModel.File = null;

            Assert.NotNull(await Edit(_documentModel.Id, documentViewModel));
        }

        [Fact]
        public async Task EditPost_ForbiddenExtensionsFileError_Test()
        {
            var documentViewModel = _documentViewModelMock.CreateWithForbiddenFileExtensions();
            documentViewModel.Id = _documentModel.Id;

            Assert.NotNull(await Edit(_documentModel.Id, documentViewModel));
        }

        [Fact]
        public async Task EditPost_ResultNull_Test()
        {
            var documentViewModel = GetDocumentViewModel();
            documentViewModel.Id = _documentModel.Id;

            _context.Documents.Remove(_documentModel);
            await _context.SaveChangesAsync();

            Assert.NotNull(await _documentsController.Edit(documentViewModel.Id, documentViewModel));
        }

        #endregion

        [Fact]
        public async Task DeleteGet_Test()
            => Assert.NotNull(await _documentsController.Delete(_documentModel.Id));

        #region - Test : DeleteConfirmed -

        [Fact]
        public async Task DeleteConfirmed_ResultFalse_Test()
        {
            var id = GenerateId();
            Assert.NotNull(await DeleteConfirmed(id));
        }

        [Fact]
        public async Task DeleteConfirmed_ResultTrue_Test()
            => Assert.NotNull(await DeleteConfirmed(_documentModel.Id));

        #endregion

        #region - Test : Download -

        [Fact]
        public async Task Download_ResultNull_Test()
        {
            var id = GenerateId();
            Assert.NotNull(await Download(id));
        }

        [Fact]
        public async Task Download_ResultNotNull_Test()
            => Assert.NotNull(await Download(_documentModel.Id));

        #endregion

        #region - Test : GetDocuments -

        [Fact]
        public async Task GetDocuments_ResultNull_Test()
            => Assert.NotNull(await GetDocuments());

        [Fact]
        public async Task GetDocuments_ResultNotNull_Test()
            => Assert.NotNull(await GetDocuments(_documentModel.Id));

        #endregion

        #region - Private methods -

        private async Task<IActionResult> Create(DocumentViewModel vm)
            => await _documentsController.Create(vm);

        private async Task<IActionResult> Edit(int id, DocumentViewModel vm)
            => await _documentsController.Edit(id, vm);

        private async Task<IActionResult> DeleteConfirmed(int id)
            => await _documentsController.DeleteConfirmed(id);

        private async Task<IActionResult> Download(int id)
            => await _documentsController.Download(id);

        private async Task<IActionResult> GetDocuments(int? id = null)
            => await _documentsController.GetDocuments(id);

        private async Task SaveDocumentModel()
        {
            _context.Documents.AddRange(new List<DocumentModel>
            {
                _documentModel,
                _documentSecondModel
            });

            await _context.SaveChangesAsync();
        }

        private DocumentViewModel GetDocumentViewModel()
            => new DocumentViewModelMock().Create();

        private int GenerateId()
            => _documentModel.Id + _autoFaker.Generate<int>();

        #endregion
    }
}
