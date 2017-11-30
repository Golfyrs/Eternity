namespace System
{
    public static class Disposable
    {
        public static IDisposable Fake = new FakeDisposable();
    }
}