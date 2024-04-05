using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace OOPClassLibrary.Fiscal;

public partial class FiscalCodeBuilder
{
    public string FiscalCodeBuild(Person person)
    {
        string fiscalCode = "";

        //tre lettere per l'individuazione del cognome
        fiscalCode = SurnameBuild(person.LastName.ToUpper());
        //tre lettere per l'individuazione del nome
        fiscalCode += NameBuild(person.FirstName.ToUpper());
        //due numeri per l'anno di nascita
        fiscalCode += YearDateBuild(person.DateOfBirth.Year);
        //una lettera per il mese di nascita
        fiscalCode += MonthDateBuild(person.DateOfBirth.Month.ToString());
        //due numeri per il giorno di nascita ed il sesso
        fiscalCode += DayDateBuild(person.DateOfBirth.Day, person.Gender);

        return fiscalCode;
    }

    private string SurnameBuild(string lastName)
    {
        /*
         * Dobbiamo ricavare 3 lettere per il cognome. Bisogna prendere la prima, la seconda e la terza consonante. 
         * Però potrebbe anche succedere che ci siano solo due consonanti oppure una sola; 
         * in tal caso dopo aver preso le consonanti si inizia a prendere anche le vocali. 
         * Se ancora mancano altre lettere per completare la nostra stringa di tre caratteri si aggiunge la lettera X.
         * I cognomi composti da più parole vanno considerati come se fossero una sola parola.
         */

        string surnameVowels = ExtractVowelsAndConsonants(lastName,true);
        string surnameConsonants = ExtractVowelsAndConsonants(lastName,false);


        //return (surnameConsonants + surnameVowels).PadRight(3, 'X').Substring(0, 3);


        if (surnameConsonants.Length > 3)
            return surnameConsonants.Substring(0, 3);

        int i = 0;
        while (surnameConsonants.Length < 3)
        {
            if (surnameVowels.Length-i > 0)
                surnameConsonants += surnameVowels[i];
            else
                surnameConsonants += "X";
            i++;
        }
        return surnameConsonants;
    }
    private string NameBuild(string fistName)
    {
        /*
         * Il procedimento che si utilizza per ricavare le tre lettere del nome, è uguale a quello del cognome 
         * con l'unica differenza che adesso dobbiamo prendere la prima, la terza e la quarta consonante. 
         * Nel caso non ci sono quattro consonanti, si prendono le prime tre e se ci sono meno di tre consonanti si effettua lo stesso procedimento del cognome.
         */

        string surnameConsonants = surnameConsonants = ExtractVowelsAndConsonants(fistName,false);
        string surnameVowels = ExtractVowelsAndConsonants(fistName, true);

        if (surnameConsonants.Length > 4)
            return surnameConsonants.Substring(0, 1) + surnameConsonants.Substring(2, 2);

        return (surnameConsonants + surnameVowels).PadRight(3, 'X').Substring(0, 3);
    }

    //I due numeri dell'anno di nascita sono semplicemente le sue ultime due cifre.
    private string YearDateBuild(int year) => (year % 100).ToString("00");

    private string MonthDateBuild(string month)
    {
        Enum.TryParse(month, out MonthFiscalCode value);
        return value.ToString();
    }

    /* Per definire il giorno di nascita ed il sesso di una persona abbiamo a disposizione 2 cifre.
     * nel caso si tratti di un uomo basta usare queste 2 cifre per indicarne semplicemente il giorno di nascita(con lo zero davanti se il giorno è di una sola cifra),
     * mentre se si tratta di una donna dobbiamo inserire il giorno di nascita sommato a 40.*/
    private string DayDateBuild(int day, Gender gender)
    {
        int dayFemale = day + 40;

        if (gender.Equals(Gender.Male))
            return day.ToString("00");

        return dayFemale.ToString("00");
    }
    private static string ExtractVowelsAndConsonants(string s, bool volwes)
    {
        char[] vowels = { 'A', 'E', 'I', 'O', 'U' };
        char[] consonants = { 'B', 'C', 'D', 'F', 'G', 'H','J','K','L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V','W','X','Y','Z' };
        char[] chars = s.ToUpper().ToCharArray();

        string charVolwes = "";
        string charConsonants = "";
        int i = 0,j = 0;

        foreach (char ch in chars)
        {
            if (vowels.Contains(ch))
            {
                charVolwes = charVolwes.Insert(i, ch.ToString());
                i++;
            }
            else if(consonants.Contains(ch))
            {
                charConsonants = charConsonants.Insert(j, ch.ToString());
                j++;
            }
        }

        if(volwes)
            return charVolwes;

        return charConsonants;
    }
}
