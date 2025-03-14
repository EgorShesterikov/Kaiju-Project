namespace Kaiju
{
    public interface IController
    {
        void PressA(bool active);
        void PressD(bool active);
        void PressW(bool active);
        void PressS(bool active);
        void PressSpace();
        void PressE();
    }
}