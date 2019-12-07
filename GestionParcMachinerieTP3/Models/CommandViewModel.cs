using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GestionParcMachinerieTP3.Models
{
    public class CommandViewModel
    {
        public CommandViewModel() { }
        public CommandViewModel(Command command, Machine machine, string email)
        {
            Id = command.Id;
            UserEmail = email;
            MachineModel = machine.Model;
            MachineId = machine.Id;
            Description = machine.Description;
            From = DateTimeHelper.DateTimeHelper.LongToDateTime(command.From).ToString("MM/dd/yyyy");
            To = DateTimeHelper.DateTimeHelper.LongToDateTime(command.To).ToString("MM/dd/yyyy");
            Status = command.Status;
        }


        public int Id { get; set; }
        [Display(Name = "User Email Address")]
        public string UserEmail { get; set; }
        [Display(Name = "Machine Id")]
        public int MachineId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Model")]
        public string MachineModel { get; set; }
        [Display(Name = "From")]
        public string From { get; set; }
        [Display(Name = "To")]
        public string To { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}