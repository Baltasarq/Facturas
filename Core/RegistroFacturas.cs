// Facturas (c) 2023 Baltasar MIT License <baltasarq@uvigo.gal>  


using System;
using System.Collections.Generic;


namespace Facturas.Core;


/// <summary>Representa una lista de facturas.</summary>
public class RegistroFacturas {
    public RegistroFacturas()
    {
        this.facturas = new List<Factura>();
    }
    
    /// <returns>El número de facturas.</summary>
    public int Count {
        get { return this.facturas.Count; }
    }

    /// <summary>Inserta una nueva factura al final</summary>
    /// <param name="f">La <see cref="Factura"/> a insertar.</param>
    public void Add(Factura f)
    {
        this.facturas.Add( f );
    }

    /// <summary>
    /// Elimina la factura en la posición <paramref name="i"/>.
    /// </summary>
    /// <param name="i">La posición de la factura en la lista.</param>
    /// <returns>Un objeto <see cref="Factura"/>.</returns>
    /// <exception cref="ArgumentException">si la posición <paramref name="i"/> es incorrecta</exception>
    public void Elimina(int i)
    {
        if ( i < 0
          || i >= this.facturas.Count )
        {
            throw new ArgumentException( $"valor de {nameof( i )}: " + i );
        }
        
        this.facturas.RemoveAt( i );
    }
    
    /// <summary>
    /// Modifica la factura en la posición <paramref name="i"/>.
    /// </summary>
    /// <param name="i">La posición de la factura en la lista.</param>
    /// <param name="bruto">El nuevo importe bruto.</param>
    /// <returns>Un objeto <see cref="Factura"/>.</returns>
    /// <exception cref="ArgumentException">si la posición <paramref name="i"/> es incorrecta</exception>
    public void Modifica(int i, double bruto)
    {
        if ( i < 0
          || i >= this.facturas.Count )
        {
            throw new ArgumentException( $"valor de {nameof( i )}: " + i );
        }
        
        this.facturas[ i ].Bruto = bruto;
    }

    /// <summary>
    /// Devuelve la factura en la posición <paramref name="i"/>.
    /// </summary>
    /// <param name="i">La posición de la factura en la lista.</param>
    /// <returns>Un objeto <see cref="Factura"/>.</returns>
    /// <exception cref="ArgumentException">si la posición <paramref name="i"/> es incorrecta</exception>
    public Factura Get(int i)
    {
        if ( i < 0
          || i >= this.facturas.Count )
        {
            throw new ArgumentException( $"valor de {nameof( i )}: " + i );
        }
        
        return this.facturas[ i ];
    }

    /// <summary>Devuelve todas las facturas</summary>
    public IList<Factura> Facturas {
        get {
            return this.facturas.AsReadOnly();      // No copia la lista
            // return new List<Factura>( this.facturas );
        }
    }

    /// <summary>Calcula el importe total sumando TODAS las facturas</summary>
    public double Total {
        get {
            double toret = 0;
            
            foreach (var f in this.facturas) {
                toret += f.Total;
            }

            return toret;
        }
    }

    List<Factura> facturas;
}
