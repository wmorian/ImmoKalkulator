namespace kalkulator.net.Services;

// Abschreibung
public class DepreciationCalcs
{
    /*
        Basis langfristige Abschreibung
        Auf diese Basis bezieht sich die langfristige Abschreibung (AfA Satz). 
        Hierzu wird der Gebäudeanteil sowohl auf den Kaufpreis als auch auf die Kaufnebenkosten angewendet. 
        Hinzu kommen "aktivierte" anfängliche Investitionen. 
        =H20*(D14+E21)+SUMIF(D25:D28;"Aktivieren";C25:C28)
    */
    public double LongtermDepreciation { get; set; }
}