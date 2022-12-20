namespace meuprojeto_web.Utils
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Remove de uma string alguns caracteres desejados.
        /// </summary>
        /// <param name="input">String de onde os caracteres serão removidos.</param>
        /// <param name="caracteres"></param>
        public static string RemoverCaracter(this string input, params string[] caracteres)
        {
            if (string.IsNullOrEmpty(input)) return input;

            if (caracteres == null || caracteres.Length == 0) return input;

            foreach (string caracter in caracteres)
            {
                input = input.Replace(caracter, string.Empty);
            }

            return input;
        }


        /// <summary>
        /// Remove os caracteres de formatação ".", ",", "-", "/", "(", ")" e " " de uma string.
        /// </summary>
        public static string RemoverFormatacao(this string input) => string.IsNullOrEmpty(input)
                ? input
                : input.RemoverCaracter(".", ",", "-", "/", "(", ")", " ").Trim();
    }
}