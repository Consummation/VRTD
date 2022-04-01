namespace VRTD.Input
{
    public interface IClickable
    {
        public void Click(IController controller);
        public void ClickDown();
        void ClickDown(IController controller);
        public void ClickUp();
    }
}