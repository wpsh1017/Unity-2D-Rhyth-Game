#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("QrUG/QqhxdocgMkhayTjd8yrCbPLF4f0WvnpxK/jXI2/NuGtAdyIEquEUMS1GvcwVJX56o2P/LnDeI0tqqWGKuQqW6Gtra2prK8uraOsnC6xGFzejN23nESGMKZk1hcBRtPkgNaEJgE7bMIReB3Ybamah4SafbckZ0+BBfpl9zMk86zDeEekKYyxpvjTkoIQMqsME3Dfe5Q2e++TTP6dFTJhe4qTXqXVIN/e9qzsisGyIy/xLq2jrJwuraauLq2trGYll9CD3sil1SDf3vas7IrBsiMv8daEJgE7bLecRIYwpmTWFwFG0+SAq4keR3EGLisuts72TaX57gmshW4yYXuKk15iKpTD7VdO5Y45IQ58Khiqul6bvK2mri6traxmJZfQg97I05KCEDKrvsFJB+4z0ZtnIayfsovr4UAC2gXpxK/jXI2/NuGtAdyIEmIqlMPtV8IReB3Ybamah4SafbckkRgIB2UO9zBUlfnqjY/8ucN4jS1CtQb9CqFO5Y45IQ58Khiqul6bvLEYXN6M3W7iBGgkp4nTuo92hIU/9CyCwJW+9CyCwJW+7IrHrq+trK2cLq2OnKHr4UAC2gUuKy62zvZNpfnuCayFbsXaHIDJIWsk43fMqwmzyxeH9Fr5nC6tjpyhqqWGKuQqW6Gtra2prK/3MyTzrMN4R6QpjLGm+KuEUMS1GquJHkdxBr7BSQfuM9GbZyGsn7KLkRgIB2UObuIEaCSnidO6j3aEhT8ME3Dfe5Q2e++TTP6dFWdPgQX6ZeyKx66vrayt");
        private static int[] order = new int[] { 5,6,4,15,8,12,15,8,11,15,26,23,25,23,16,24,21,27,19,22,28,21,26,28,25,26,26,28,28,29 };
        private static int key = 172;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
