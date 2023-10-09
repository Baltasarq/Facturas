// Facturas (c) 2023 Baltasar MIT License <baltasarq@uvigo.gal>  


namespace Facturas.Core;


/// <summary>Representa una factura con el importe bruto y total calculado.</summary>
public class Factura {
    /// <summary>El importe bruto de la factura.</summary>
    public required double Bruto { get; set; }
    
    /// <summary>El nombre de la empresa.</summary>
    public required string Empresa { get; init; }
    
    /// <summary>Calcula el total, dado el importe bruto.</summary>
    public double Total {
        get {
            const double IVA = 0.21;
            return this.Bruto * ( 1.0 + IVA );    
        }
    }
}
