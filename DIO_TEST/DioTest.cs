using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DIO_TEST
{
    public class DioTest
    {
        private readonly Mock<DbSet<DIO.Models.Category>> _mockSet;
        private readonly Mock<DIO.Models.Context> _mockContext;
        private readonly DIO.Models.Category _categoria;
        private int _testID = 1;
        public DioTest()
        {
            _mockSet = new Mock<DbSet<DIO.Models.Category>>();
            _mockContext = new Mock<DIO.Models.Context>();
            _categoria = new DIO.Models.Category { Id = _testID, Description = "Test Food Category" };

            _mockContext.Setup(m => m.Categories).Returns(_mockSet.Object);

            _mockContext.Setup(m => m.Categories.FindAsync(_testID)).ReturnsAsync(_categoria);

            _mockContext.Setup(m => m.SetModified(_categoria));

            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_testID);
        }

        [Xunit.Fact]
        public async Task GetCategoryTest()
        {
            var service = new API_DIO.Controllers.CategoriesController(_mockContext.Object);

            var tested_category = await service.GetCategory(_testID);

            _mockSet.Verify(m => m.FindAsync(_testID), Times.Once());

            Xunit.Assert.Equal(_categoria.Id, tested_category.Value.Id);

        }

        [Xunit.Fact]
        public async Task PutCategoryTest()
        {
            var service = new API_DIO.Controllers.CategoriesController(_mockContext.Object);

            await service.PutCategory(_testID,_categoria);

            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        }

        [Xunit.Fact]
        public async Task PostCategoryTest()
        {
            var service = new API_DIO.Controllers.CategoriesController(_mockContext.Object);

            await service.PostCategory(_categoria);

            _mockSet.Verify(x => x.Add(_categoria), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        }

        [Xunit.Fact]
        public async Task DeleteCategoryTest()
        {
            var service = new API_DIO.Controllers.CategoriesController(_mockContext.Object);

            await service.DeleteCategory(_testID);

            _mockSet.Verify(m => m.FindAsync(_testID), Times.Once());
            _mockSet.Verify(x => x.Remove(_categoria), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        }

    }
}
