using System;
using System.Collections.Generic;

namespace InterfazPFMCFIC.Models;

public partial class InterfazPfmCficCatTipoProducto
{
    public int CatTipoProductoId { get; set; }

    public string DescripcionProducto { get; set; } = null!;

    public bool Pfmcfic { get; set; }

    public bool Inactivo { get; set; }

    public bool? Personas { get; set; }

    public bool? Vehículos { get; set; }

    public bool? ArmasyExplosivos { get; set; }

    public bool? MaterialApocrifo { get; set; }

    public bool? BienesInmuebles { get; set; }

    public bool? Numeral { get; set; }

    public bool? Drogas { get; set; }

    public bool? EquipoDeComunicacion { get; set; }

    public bool? EquipoTactico { get; set; }

    public bool? FaunaProtegida { get; set; }

    public bool? Ubicacion { get; set; }

    public bool? Otros { get; set; }

    public bool? Archivo { get; set; }

    public bool? PersonasMorales { get; set; }

    public bool? Alias { get; set; }

    public bool? OrganizacionesDelictivas { get; set; }

    public bool? Eventos { get; set; }

    public bool? InternetuOtros { get; set; }

    public bool? CuentasBancarias { get; set; }

    public bool? Domicilios { get; set; }

    public bool? Expediente { get; set; }

    public bool? DispositivodeAlmacenamiento { get; set; }
}
