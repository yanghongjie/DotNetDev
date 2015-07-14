using System;

namespace Test.Common.Database.Init
{
    public static class TestInit
    {
        public static void SetDataDirectory()
        {
            var p = AppDomain.CurrentDomain.BaseDirectory;
            if (p.IndexOf("\\bin\\", StringComparison.Ordinal) > 0)
            {
                if (p.EndsWith("\\bin\\Debug"))
                    p = p.Replace("\\bin\\Debug", "");
                if (p.EndsWith("\\bin\\Release"))
                    p = p.Replace("\\bin\\Release", "");
            }
            AppDomain.CurrentDomain.SetData("DataDirectory", p);
        }
    }
}