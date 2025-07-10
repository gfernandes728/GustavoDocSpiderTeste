using AutoBogus;
using GustavoDocSpiderTeste.Business.Documents;
using GustavoDocSpiderTeste.Data.Data;
using GustavoDocSpiderTeste.Models.Models;
using GustavoDocSpiderTeste.UnitTests.Mock;
using GustavoDocSpiderTeste.ViewModels;
using GustavoDocSpiderTeste.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GustavoDocSpiderTeste.UnitTests.Business
{
    public class DocumentsBusinessTest
    {
        private readonly IAutoFaker _autoFaker;
        private readonly ApplicationDbContext _context;
        private readonly DocumentsBusiness _documentsBusiness;

        private readonly DocumentModel _documentModel;
        private readonly DocumentModel _documentModelFileDataNull;

        private readonly DocumentViewModel _documentViewModel;
        private readonly DocumentViewModel _documentViewModelFileNull;

        public DocumentsBusinessTest()
        {
            _autoFaker = AutoFaker.Create();

            _context = ApplicationDbContextMock.Create();
            _documentsBusiness = new DocumentsBusiness(_context);

            var documentModelMock = new DocumentModelMock();
            _documentModel = documentModelMock.Create();
            _documentModelFileDataNull = documentModelMock.CreateWithFileData(null);

            var documentViewModelMock = new DocumentViewModelMock();
            _documentViewModel = documentViewModelMock.Create();
            _documentViewModelFileNull = documentViewModelMock.CreateWithFile(null);

            SaveDocumentModel().Wait();
        }

        #region - Test : GetListDocuments -

        [Fact]
        public async Task GetListDocuments_SearchNull_Test()
            => Assert.NotNull(await GetListDocuments("", _autoFaker.Generate<int>(), _autoFaker.Generate<int>(), ""));

        [Fact]
        public async Task GetListDocuments_SearchNotNull_Test()
            => Assert.NotNull(await GetListDocuments(_documentModel.Title, _autoFaker.Generate<int>(), _autoFaker.Generate<int>(), ""));

        [Fact]
        public async Task GetListDocuments_OrdeByNotNull_Test()
            => Assert.NotNull(await GetListDocuments(_documentModel.Title, _autoFaker.Generate<int>(), _autoFaker.Generate<int>(), "Description asc"));

        [Fact]
        public async Task GetListDocuments_PageSize_Test()
            => Assert.NotNull(await GetListDocuments(_documentModel.Title, 1, 1, "Description asc"));

        #endregion

        #region - Test : GetDocument -

        [Fact]
        public async Task GetDocumentNull_Test()
            => Assert.Null(await GetDocument());

        [Fact]
        public async Task GetDocumentNotNull_Test()
            => Assert.NotNull(await GetDocument(_documentModel.Id));

        #endregion

        #region - Test : GetDocumentForDownload -

        [Fact]
        public async Task GetDocumentForDownloadNotNull_Test()
            => Assert.NotNull(await GetDocumentForDownload(_documentModel.Id));

        [Fact]
        public async Task GetDocumentForDownloadNull_DocumentNull_Test()
            => Assert.Null(await GetDocumentForDownload());

        [Fact]
        public async Task GetDocumentForDownloadNull_FileDataNull_Test()
            => Assert.Null(await GetDocumentForDownload(_documentModelFileDataNull.Id));

        #endregion

        #region - Test : HasDocumentTitle -

        [Fact]
        public async Task HasDocumentTitleReturnsTrue_Test()
            => Assert.True(await HasDocumentTitle(_documentModel.Title));

        [Fact]
        public async Task HasDocumentTitleReturnsFalse_Test()
        {
            var title = GenerateTitle();
            Assert.False(await HasDocumentTitle(title));
        }

        #endregion

        #region - Test : HasDocumentTitleForEdit -

        [Fact]
        public async Task HasDocumentTitleForEditTrue_Test()
        {
            var id = GenerateId();
            Assert.True(await HasDocumentTitleForEdit(_documentModel.Title, id));
        }

        [Fact]
        public async Task HasDocumentTitleReturnsFalse_DifferentTitleAndDifferentId_Test()
        {
            var title = GenerateTitle();
            var id = GenerateId();
            Assert.False(await HasDocumentTitleForEdit(title, id));
        }

        [Fact]
        public async Task HasDocumentTitleReturnsFalse_DifferentTitleAndSameId_Test()
        {
            var title = GenerateTitle();
            Assert.False(await HasDocumentTitleForEdit(title, _documentModel.Id));
        }

        [Fact]
        public async Task HasDocumentTitleReturnsFalse_SameTitleAndSameId_Test()
            => Assert.False(await HasDocumentTitleForEdit(_documentModel.Title, _documentModel.Id));

        #endregion

        [Fact]
        public async Task CreateDocument_Test()
            => Assert.IsType<bool>(await _documentsBusiness.CreateDocument(_documentViewModel));

        #region - Test : DeleteDocument -

        [Fact]
        public async Task DeleteDocumentFalse_Test()
            => Assert.False(await DeleteDocument());

        [Fact]
        public async Task DeleteDocumentTrue_Test()
            => Assert.True(await DeleteDocument(_documentModel.Id));

        #endregion

        #region - Test : EditDocument -

        [Fact]
        public async Task EditDocumentNull_Test()
            => Assert.Null(await EditDocument(_documentViewModel));

        [Fact]
        public async Task EditDocumentNotNull_FileNull_Test()
            => Assert.NotNull(await EditDocument(_documentViewModelFileNull, _documentModel.Id));

        [Fact]
        public async Task EditDocumentNotNull_FileNotNull_Test()
            => Assert.NotNull(await EditDocument(_documentViewModel, _documentModel.Id));

        #endregion

        #region - Test : IsForbiddenFileExtensionsFile -

        [Fact]
        public void IsForbiddenFileExtensionsFileFalse_Test()
            => Assert.False(IsForbiddenFileExtensions(".txt"));

        [Fact]
        public void IsForbiddenFileExtensionsFileTrue_FileExe_Test()
            => Assert.True(IsForbiddenFileExtensions(".exe"));

        [Fact]
        public void IsForbiddenFileExtensionsFileTrue_FileZip_Test()
            => Assert.True(IsForbiddenFileExtensions(".zip"));

        [Fact]
        public void IsForbiddenFileExtensionsFileTrue_FileBat_Test()
            => Assert.True(IsForbiddenFileExtensions(".bat"));

        #endregion

        #region - Test : GetDocumentModel -

        [Fact]
        public async Task GetDocumentModelNull_IdNull_Test()
            => Assert.Null(await GetDocumentModel());

        [Fact]
        public async Task GetDocumentModelNull_DocumentNull_Test()
        {
            var id = GenerateId();
            Assert.Null(await GetDocumentModel(id));
        }

        [Fact]
        public async Task GetDocumentModelNotNull_Test()
            => Assert.NotNull(await GetDocumentModel(_documentModel.Id));

        #endregion

        [Fact]
        public void GetDocumentViewModel_Test()
            => Assert.NotNull(_documentsBusiness.GetDocumentViewModel(_documentModel));

        #region - Private methods -

        private async Task SaveDocumentModel()
        {
            _context.Documents.AddRange(new List<DocumentModel>
            {
                _documentModel,
                _documentModelFileDataNull
            });

            await _context.SaveChangesAsync();
        }

        private async Task<PagedResult<DocumentViewModel>> GetListDocuments(string search, int pageNumber, int pageSize, string orderBy)
            => await _documentsBusiness.GetListDocuments(search, pageNumber, pageSize, orderBy);

        private async Task<DocumentViewModel> GetDocument(int? id = null)
            => await _documentsBusiness.GetDocument(id);

        private async Task<DocumentModel> GetDocumentForDownload(int? id = null)
            => await _documentsBusiness.GetDocumentForDownload(id);

        private async Task<bool> HasDocumentTitle(string title)
            => await _documentsBusiness.HasDocumentTitle(title);

        private async Task<bool> HasDocumentTitleForEdit(string title, int id)
            => await _documentsBusiness.HasDocumentTitleForEdit(title, id);

        private async Task<bool> DeleteDocument(int? id = null)
            => await _documentsBusiness.DeleteDocument(id);

        private async Task<bool?> EditDocument(DocumentViewModel vm, int? id = null)
            => await _documentsBusiness.EditDocument(id, vm);

        private bool IsForbiddenFileExtensions(string fileExtension)
        {
            var fileName = GenerateFileName(fileExtension);
            return _documentsBusiness.IsForbiddenExtensionsFile(fileName);
        }

        private async Task<DocumentModel> GetDocumentModel(int? id = null)
            => await _documentsBusiness.GetDocumentModel(id);

        private int GenerateId()
            => _documentModel.Id + _autoFaker.Generate<int>();

        private string GenerateTitle() 
            => $"{_documentModel.Title}_{_autoFaker.Generate<string>()}";

        private string GenerateFileName(string fileExtension)
            => $"{_autoFaker.Generate<string>()}.{fileExtension}";

        #endregion
    }
}
