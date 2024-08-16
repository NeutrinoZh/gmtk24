namespace GMTK.Services
{
    public class InputController : IService
    {
        public Actions Actions { get; private set; }

        public InputController()
        {
            Actions = new Actions();
            Actions.Enable();
        }
    }
}