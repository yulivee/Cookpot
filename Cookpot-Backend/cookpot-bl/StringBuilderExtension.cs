using System;
using System.Text;

namespace cookpot.bl
{
    public static class StringBuilderExtension {
            public static void conditionalAppend<T>( this StringBuilder builder, string property, T value) {

                if (value == null) return;
                builder.AppendLine($"cp:{property} {value};");

        }
    }
}