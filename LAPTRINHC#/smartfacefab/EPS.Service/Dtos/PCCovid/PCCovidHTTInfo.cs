using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Service.Dtos
{
    public class PCCovidHTTInfo
    {
        public PCCovidKBCN KBCN{get;set;}
        public Object KBND{get;set;}
        public Object QR6Info{get;set;}
        public string userId{get;set;}
        public string fullName{get;set;}
        public string phone{get;set;}
        public string gender{get;set;}
        public Nullable<int> yearOfBirthday{get;set;}
        public string checkinId{get;set;}
        public Nullable<bool> checkReason{get;set;}
        public string temporaryToken{get;set;}
        public string userStatus{get;set;}
        public Nullable<int> configDeclaration{get;set;}
        public Nullable<int> healthStatus{get;set;}
        public Nullable<bool> checkDeclaration{get;set;}
        public Object healthState{get;set;}
        public Nullable<bool> inEpidemicArea{get;set;}
        public Nullable<bool> contactHistory{get;set;}
        public Nullable<bool> travelHistory{get;set;}
        public long lastTime{get;set;}
        public PCCovidHistoryInjection historyInjection{get;set;}
        public List<PCCovidHistoryInjection> historyInjectionList{get;set;}
        public PCCovidCovidTest covidTest{get;set;}
        public Object locationTravels{get;set;}
    }
}
