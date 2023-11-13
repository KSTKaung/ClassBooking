using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClassBooking.Models
{
    public class ConstantData
    {
        //Alphabetical list of countries
        public enum Country
        {
            Afghanistan = 1,
            Albania,
            Algeria,
            Andorra,
            Angola,
            [Display(Name = "Antigua Barbuda")]
            Antigua_Barbuda,
            Argentina,
            Armenia,
            Australia,
            Austria,
            Azerbaijan,
            Bahamas,
            Bahrain,
            Bangladesh,
            Barbados,
            Belarus,
            Belgium,
            Belize,
            Benin,
            Bhutan,
            Bolivia,
            [Display(Name = "Bosnia Herzegovina")]
            Bosnia_Herzegovina,
            Botswana,
            Brazil,
            Brunei,
            Bulgaria,
            BurkinaFaso,
            Burundi,
            CôtedIvoire,
            [Display(Name = "Cabo Verde")]
            Cabo_Verde,
            Cambodia,
            Cameroon,
            Canada,
            [Display(Name = "Central African Republic")]
            Central_African_Republic,
            Chad,
            Chile,
            China,
            Colombia,
            Comoros,
            [Display(Name = "Congo (Congo-Brazzaville)")]
            Congo,
            [Display(Name = "Costa Rica")]
            Costa_Rica,
            Croatia,
            Cuba,
            Cyprus,
            [Display(Name = "Czechia (Czech Republic)")]
            Czechia,
            [Display(Name = "Democratic Republic of the Congo")]
            Democratic_Republic_of_the_Congo,
            Denmark,
            Djibouti,
            Dominica,
            [Display(Name = "Dominican Republic")]
            Dominican_Republic,
            Ecuador,
            Egypt,
            [Display(Name = "El Salvador")]
            El_Salvador,
            [Display(Name = "Equatorial Guinea")]
            Equatorial_Guinea,
            Eritrea,
            Estonia,
            Eswatini,
            Ethiopia,
            Fiji,
            Finland,
            France,
            Gabon,
            Gambia,
            Georgia,
            Germany,
            Ghana,
            Greece,
            Grenada,
            Guatemala,
            Guinea,
            [Display(Name = "Guinea-Bissau")]
            Guinea_Bissau,
            Guyana,
            Haiti,
            [Display(Name = "Holy See")]
            Holy_See,
            Honduras,
            Hungary,
            Iceland,
            India,
            Indonesia,
            Iran,
            Iraq,
            Ireland,
            Israel,
            Italy,
            Jamaica,
            Japan,
            Jordan,
            Kazakhstan,
            Kenya,
            Kiribati,
            Kuwait,
            Kyrgyzstan,
            Laos,
            Latvia,
            Lebanon,
            Lesotho,
            Liberia,
            Libya,
            Liechtenstein,
            Lithuania,
            Luxembourg,
            Madagascar,
            Malawi,
            Malaysia,
            Maldives,
            Mali,
            Malta,
            [Display(Name = "Marshall Islands")]
            Marshall_Islands,
            Mauritania,
            Mauritius,
            Mexico,
            Micronesia,
            Moldova,
            Monaco,
            Mongolia,
            Montenegro,
            Morocco,
            Mozambique,
            Myanmar,
            Namibia,
            Nauru,
            Nepal,
            Netherlands,
            [Display(Name = "New Zealand")]
            New_Zealand,
            Nicaragua,
            Niger,
            Nigeria,
            [Display(Name = "North Korea")]
            North_Korea,
            [Display(Name = "North Macedonia")]
            North_Macedonia,
            Norway,
            Oman,
            Pakistan,
            Palau,
            [Display(Name = "Palestine State")]
            Palestine_State,
            Panama,
            [Display(Name = "Papua New Guinea")]
            Papua_New_Guinea,
            Paraguay,
            Peru,
            Philippines,
            Poland,
            Portugal,
            Qatar,
            Romania,
            Russia,
            Rwanda,
            [Display(Name = "Saint Kitts and Nevis")]
            Saint_Kitts_Nevis,
            [Display(Name = "Saint Lucia")]
            Saint_Lucia,
            [Display(Name = "Saint Vincent and the Grenadines")]
            Saint_Vincent_Grenadines,
            Samoa,
            [Display(Name = "San Marino")]
            San_Marino,
            [Display(Name = "Sao Tome and Principe")]
            Sao_Tome_Principe,
            [Display(Name = "Saudi Arabia")]
            Saudi_Arabia,
            Senegal,
            Serbia,
            Seychelles,
            [Display(Name = "Sierra Leone")]
            Sierra_Leone,
            Singapore,
            Slovakia,
            Slovenia,
            [Display(Name = "Solomon Islands")]
            Solomon_Islands,
            Somalia,
            [Display(Name = "South Africa")]
            South_Africa,
            [Display(Name = "South Korea")]
            South_Korea,
            [Display(Name = "South Sudan")]
            South_Sudan,
            Spain,
            [Display(Name = "Sri Lanka")]
            Sri_Lanka,
            Sudan,
            Suriname,
            Sweden,
            Switzerland,
            Syria,
            Tajikistan,
            Tanzania,
            Thailand,
            [Display(Name = "Timor-Leste")]
            Timor_Leste,
            Togo,
            Tonga,
            [Display(Name = "Trinidad and Tobago")]
            Trinidad_Tobago,
            Tunisia,
            Turkey,
            Turkmenistan,
            Tuvalu,
            Uganda,
            Ukraine,
            [Display(Name = "United Arab Emirates")]
            United_Arab_Emirates,
            [Display(Name = "United Kingdom")]
            United_Kingdom,
            [Display(Name = "United States of America")]
            USA,
            Uruguay,
            Uzbekistan,
            Vanuatu,
            Venezuela,
            Vietnam,
            Yemen,
            Zambia,
            Zimbabwe
        }

        public static string UserID = "_UserID";
        public static string UserCountry = "_UserCountry";
        public static string UserRemainCredit = "_UserRemainCredit";
    }
}
