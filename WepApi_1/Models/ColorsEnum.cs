using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WepApi_1.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ColorsEnum
    {
        red,
        blue,
        yellow
    }

    //public class ColorsEnum
    //{
    //    private ColorsEnum(string value) { Value = value; }

    //    public string Value { get; private set; }

    //    public static ColorsEnum Red { get { return new ColorsEnum("Red"); } }
    //    public static ColorsEnum Blue { get { return new ColorsEnum("Blue"); } }
    //    public static ColorsEnum Yellow { get { return new ColorsEnum("Yellow"); } }
    //    public static ColorsEnum Brown { get { return new ColorsEnum("Brown"); } }
    //    public static ColorsEnum Black { get { return new ColorsEnum("Black"); } }
    //    public static ColorsEnum White { get { return new ColorsEnum("White"); } }
    //}
}