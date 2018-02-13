using Prism.Navigation;

namespace ToDoListApp.XAML.Behaviors
{
    public interface INavigationServiceManager
    {
        void Attach(INavigationService ns);
    }
}