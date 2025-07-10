using GustavoDocSpiderTeste.Data.Data;
using GustavoDocSpiderTeste.Models.Models;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GustavoDocSpiderTeste.Business.Documents
{
    public class DocumentsBusiness
    {
        private readonly ApplicationDbContext _context;

        private readonly string[] _forbiddenExtensions = { ".exe", ".zip", ".bat" };

        public DocumentsBusiness(ApplicationDbContext context)
            => _context = context;

        #region - Public methods -

        public async Task<ViewModels.PagedResult<DocumentViewModel>> GetListDocuments(string search, 
            int pageNumber, 
            int pageSize, 
            string orderBy)
        {
            var query = _context.Documents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d => 
                    d.Title.Contains(search) ||
                    d.Description.Contains(search) ||
                    d.FileName.Contains(search));
            }

            var totalItems = await query.CountAsync();

            query = string.IsNullOrWhiteSpace(orderBy) ? query.OrderByDescending(d => d.CreatedAt) : query.OrderBy(orderBy);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var documentViewModels = items.Select(d => GetDocumentViewModel(d)).ToList();

            return new ViewModels.PagedResult<DocumentViewModel>
            {
                Items = items.Select(d => GetDocumentViewModel(d)).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<DocumentViewModel> GetDocument(int? id)
        {
            var document = await GetDocumentModel(id);
            return document == null ? null : GetDocumentViewModel(document);
        }

        public async Task<DocumentModel> GetDocumentForDownload(int? id)
        {
            var document = await GetDocumentModel(id);
            return (document == null || document.FileData == null) ? null : document;
        }

        public async Task<bool> HasDocumentTitle(string title)
            => await _context.Documents.AnyAsync(d => d.Title == title);

        public async Task<bool> HasDocumentTitleForEdit(string title, int id)
            => await _context.Documents.AnyAsync(d => d.Title == title && d.Id != id);

        public async Task<bool> CreateDocument(DocumentViewModel document)
        {
            var doc = new DocumentModel
            {
                Title = document.Title,
                Description = document.Description,
                FileName = document.File.FileName,
                FileData = await GetFileBytesAsync(document.File),
                CreatedAt = DateTime.Now
            };

            _context.Add(doc);
            return await SaveChanges();
        }

        public async Task<bool> DeleteDocument(int? id)
        {
            var document = await GetDocumentModel(id);
            if (document == null)
                return false;

            _context.Documents.Remove(document);
            return await SaveChanges();
        }

        public async Task<bool?> EditDocument(int? id, DocumentViewModel vm)
        {
            var document = await GetDocumentModel(id);
            if (document == null)
                return null;

            document.Title = vm.Title;
            document.Description = vm.Description;

            if (vm.File != null)
            {
                document.FileName = vm.File.FileName;
                document.FileData = await GetFileBytesAsync(vm.File);
            }

            _context.Update(document);
            return await SaveChanges();
        }

        public bool IsForbiddenExtensionsFile(string fileName)
            => _forbiddenExtensions.Contains(Path.GetExtension(fileName).ToLower());

        #endregion

        #region - Auxiliar methods -

        public async Task<DocumentModel> GetDocumentModel(int? id)
        {
            if (!id.HasValue)
                return null;

            var document = await _context.Documents.FindAsync(id);
            return document ?? null;
        }

        public DocumentViewModel GetDocumentViewModel(DocumentModel document)
        {
            return new DocumentViewModel
            {
                Id = document.Id,
                Title = document.Title,
                Description = document.Description,
                FileName = document.FileName
            };
        }

        private async Task<bool> SaveChanges()
            => await _context.SaveChangesAsync() == 1;

        private async Task<byte[]> GetFileBytesAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        #endregion
    }
}
