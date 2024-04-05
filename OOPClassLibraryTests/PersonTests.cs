using OOPClassLibrary.Fiscal;

namespace OOPClassLibraryTests;

public class PersonTests
{

    [Theory]
    [MemberData(nameof(GetValues))]
    public void CanAddTheoryMemberDataProperty(
        string codFis,
        string firstName,
        string lastName,
        string placeOfBirth,
        DateOnly date,
        Gender g,
        MaritalStatus m
        )
    {
        Person person = new Person(firstName, lastName, placeOfBirth, date, g, m);
        FiscalCodeBuilder fiscalCodeBuilder = new OOPClassLibrary.Fiscal.FiscalCodeBuilder();

        Assert.Equal(codFis, fiscalCodeBuilder.FiscalCodeBuild(person).Substring(0, 11));
    }

    public static IEnumerable<object[]> GetValues =>
        new List<object[]>
        {
            new object[] { "FRCLNU89E67","Luana", "Fracassi","Roma",new DateOnly(1989,05,27),Gender.Female,MaritalStatus.Unmarried},
            new object[] { "PRRSRA93B64","SARA", "PIRRETTI","Roma", new DateOnly(1993,02,24),Gender.Female,MaritalStatus.Unmarried},
            new object[] { "FOXDRA80A01", "dario", "fo","Roma", new DateOnly(1980,01,01), Gender.Male, MaritalStatus.Unmarried }
        };
}

