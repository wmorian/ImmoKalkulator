namespace kalkulator.net.Services.Precalculation;

// Rücklagen
public class ReservesCalcs
{
    /*  
        Empfohlene Instandhalt.-Rückl.
        Bei einer eigenen Rückl. in dieser Höhe, ergibt sich zusammen mit der WEG-Rücklage 
        eine solide Gesamtrücklage gemäß der sog. Petersschen Formel. 
        Voraussetzung: Der Gebäudeanteil am Kaufpreis wurde mit der 
        entspr. Vorlage vom Finanzministerium ermittelt.  
    */
    public double RecommendedMaintanceReserves { get; set; }
}