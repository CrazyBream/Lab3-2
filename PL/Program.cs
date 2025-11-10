using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DAL.DataProviders;
using Core.Entities;
using BLL.Services;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var jsonProvider = new JsonDataProvider();
            var context = new ParticipantContext(jsonProvider);
            var validationService = new ValidationService();

            var participantService = new ParticipantService(context, jsonProvider, validationService);

            var menu = new Menu(participantService);
            menu.ShowMainMenu();
        }
    }
}