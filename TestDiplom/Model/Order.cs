//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestDiplom.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderItem = new HashSet<OrderItem>();
        }
    
        public int IdOrder { get; set; }
        public Nullable<int> IdGuest { get; set; }
        public Nullable<int> IdPersonal { get; set; }
        public Nullable<int> IdTable { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
    
        public virtual Guest Guest { get; set; }
        public virtual Personal Personal { get; set; }
        public virtual Table Table { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
