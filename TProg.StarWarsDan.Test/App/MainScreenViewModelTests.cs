using Moq;

using TProg.StarWarsDan.App.ViewModels;
using TProg.StarWarsDan.Ui.Api;

namespace TProg.StarWarsDan.Test.App;

[TestFixture]
public class MainScreenViewModelTests
{
    private Mock<IPersonListViewModel> mockPersonListViewModel;
    private MainScreenViewModel viewModel;

    [SetUp]
    public void SetUp()
    {
        this.mockPersonListViewModel = new Mock<IPersonListViewModel>();
        this.viewModel = new MainScreenViewModel(this.mockPersonListViewModel.Object);
    }

    [Test]
    public void PersonListViewModel_ShouldReturnInjectedInstance() => Assert.That(this.viewModel.PersonListViewModel, Is.EqualTo(this.mockPersonListViewModel.Object));

    [Test]
    public void Dispose_ShouldCallDisposeOnPersonListViewModel()
    {
        this.viewModel.Dispose();
        this.mockPersonListViewModel.Verify(m => m.Dispose(), Times.Once);
    }
}
