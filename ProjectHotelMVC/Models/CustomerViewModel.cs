using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotelMVC.Models
{
    public class CustomerViewModel
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [Key]
        [Required]
        public Guid ID { get; set; }
        /// <summary>
        /// Имя.
        /// </summary>
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя обязательно к заполнению!")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия.
        /// </summary>
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательно к заполнению!")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        /// <summary>
        /// Отчество.
        /// </summary>
        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Отчество обязательно к заполнению!")]
        [DataType(DataType.Text)]
        public string Patronymic { get; set; }
        /// <summary>
        /// Вычисляемое свойство.ФИО клиента.
        /// </summary>
        [DataType(DataType.Text)]
        [NotMapped]
        public string FullName { get { return $"{LastName} {FirstName} {Patronymic}"; } }
        /// <summary>
        /// Номер телефона.
        /// </summary>
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Номер телефона обязательно к заполнению!")]
        [DataType(DataType.PhoneNumber)]
        public long? PhoneNumber { get; set; }
        /// <summary>
        /// Номер паспорта.
        /// </summary>
        [Required(ErrorMessage = "Номер паспорта обязательно к заполнению!")]
        [Display(Name = "Номер паспорта")]
        [DataType(DataType.Text)]
        public string PassportID { get; set; }
        /// <summary>
        /// Список объектов класса BookingIfo содержащих информацию о бронировании номера (Кем забронирован, на какую дату, и тд).
        /// </summary>
        public virtual ICollection<BookingInfoViewModel> BookingInfos { get; set; }
        public CustomerViewModel()
        {
            this.ID = Guid.NewGuid();
            BookingInfos = new List<BookingInfoViewModel>();
        }
    }
}
