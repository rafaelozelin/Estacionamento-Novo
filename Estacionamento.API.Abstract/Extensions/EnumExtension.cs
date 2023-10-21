using System.ComponentModel;

namespace Estacionamento.API.Abstract.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T source)
        {
            if (source == null)
                return string.Empty;

            var field = source.GetType().GetField(source.ToString() ?? "");

            if (field == null)
                return string.Empty;

            var attributes = (DescriptionAttribute[])
                field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;

            return source.ToString() ?? "";
        }
    }
}
