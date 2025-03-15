namespace Kaiju
{
    public interface IController
    {
        void PressInstantVertical(float value);
        void PressInstantHorizontal(float value);
        void PressSpace();
        void PressE();
    }
}