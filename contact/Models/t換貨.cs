//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace contact.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class t換貨
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t換貨()
        {
            this.t換貨明細 = new HashSet<t換貨明細>();
        }
    
        public int f換貨單號ID { get; set; }
        public Nullable<int> f店家ID { get; set; }
        public string f申請日期 { get; set; }
        public string f連絡電話 { get; set; }
        public string f換貨申請人 { get; set; }
        public string f換貨原因 { get; set; }
        public string f換貨申請狀態 { get; set; }
        public string f對外訂單單號 { get; set; }
        public Nullable<int> f訂單明細ID { get; set; }
        public string f換貨備註 { get; set; }
        public string f售後服務申請對外Id { get; set; }
    
        public virtual t店家 t店家 { get; set; }
        public virtual t訂單明細 t訂單明細 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t換貨明細> t換貨明細 { get; set; }
    }
}
