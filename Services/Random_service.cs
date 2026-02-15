using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using Microsoft.Identity.Client;

namespace exe1.Services
{
    public class Random_service : IRandomService
    {
        private readonly IRandomRepository repository;

        public Random_service(IRandomRepository repository)
        {
            this.repository = repository;
        }


        //theRandomPrize
        public async Task<User> TheRandomPrize(int prizeId)
        {
            // קוראים לרפוזיטורי ומקבלים רשימת כרטיסים
            var ticketsList = await repository.TheRandomPrize(prizeId);

            if (ticketsList == null || !ticketsList.Any())
            {
                return null; // אף אחד לא שילם, לכן אין זוכה
            }

            // הגרלה רנדומלית
            var random = new Random();
            var winnerTicket = ticketsList[random.Next(ticketsList.Count)];

            var winnerUser = winnerTicket.User;

            // עדכון הפרס במסד הנתונים (שיוך זוכה וסגירה)
            var prize = await repository.findprizeById(prizeId);
            if (prize != null)
            {
                prize.Thewinner = winnerUser;
                prize.IsActive = false;
                await repository.SavetheChanges();
            }

            return winnerUser;
        }
        public async Task<IEnumerable<DtoWinnersReport>> GetWinnerReport()
        {
            var prizes =  await repository.GetWinnerReport();
            List<DtoWinnersReport> winnersReports = new List<DtoWinnersReport>();
            foreach (var p in prizes)
                if(p.Thewinner != null) { 
                {
                DtoWinnersReport dtoWinnersReport = new DtoWinnersReport();
                dtoWinnersReport.prize = p;
                dtoWinnersReport.user = p.Thewinner;
                 winnersReports.Add(dtoWinnersReport);
                    }
        }
            return winnersReports;

        }

    }
}
