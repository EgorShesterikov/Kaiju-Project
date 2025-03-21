namespace Kaiju
{
    public interface IController
    {
        void StartControl() { }
        void PressInstantVertical(float value);
        void PressInstantHorizontal(float value);
        void PressSpace(bool active);
        void PressE();
    }
}