
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace ElectricsOnlineWebApp
{

using System;
    using System.Collections.Generic;
    
public partial class Product
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Product()
    {

        this.Order_Products = new HashSet<Order_Products>();

    }


    public int PID { get; set; }

    public string PName { get; set; }

    public decimal UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public string Category { get; set; }

    public string Description { get; set; }

    public int SID { get; set; }

    public int ROL { get; set; }

    public string Brand { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Order_Products> Order_Products { get; set; }

}

}
