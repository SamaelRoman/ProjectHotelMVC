using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Models
{
    public class BookingInfoViewModel
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [Key]
        [Required]
        [Display(Name = "ID брони")]
        public Guid ID { get; set; }
        /// <summary>
        /// Дата начала бронирования.
        /// </summary>
        [Display(Name = "Дата заселения!")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartBooking { get; set; }
        /// <summary>
        /// Дата окончания бронирвоания.
        /// </summary>
        [Display(Name = "Дата выселения!")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndBooking { get; set; }
        /// <summary>
        /// Номер для бронирования!
        /// </summary>
        [JsonIgnore]
        public virtual RoomViewModel Room { get; set; }
        [Required]
        [Display(Name = "Номер комнаты")]
        public string RoomID { get; set; }
        /// <summary>
        /// Постоялец забронировавший номер.
        /// </summary>
        [JsonIgnore]
        public virtual CustomerViewModel Customer { get; set; }
        [Required]
        public Guid CustomerID { get; set; }
        /// <summary>
        /// Итоговая цена.
        /// </summary>
        [Display(Name = "Итоговая цена")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get;set; }
        public BookingInfoViewModel()
        {
            this.ID = Guid.NewGuid();
        }
    }
    public class BookingInfoRegViewModel
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [Key]
        [Required]
        public Guid ID { get; set; }
        /// <summary>
        /// Дата начала бронирования.
        /// </summary>
        [Display(Name = "Дата заселения!")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartBooking { get; set; }
        /// <summary>
        /// Дата окончания бронирвоания.
        /// </summary>
        [Display(Name = "Дата выселения!")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndBooking { get; set; }
        /// <summary>
        /// Номер для бронирования!
        /// </summary>
        [JsonIgnore]
        public virtual RoomViewModel Room { get; set; }
        [Required]
        public string RoomID { get; set; }
        /// <summary>
        /// Постоялец забронировавший номер.
        /// </summary>
        [Required]
        public virtual CustomerViewModel Customer { get; set; }
        public Guid CustomerID { get; set; }
        /// <summary>
        /// Итоговая цена.
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get;set; }
        public BookingInfoRegViewModel()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
