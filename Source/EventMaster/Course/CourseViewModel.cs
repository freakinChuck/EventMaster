﻿using EventMaster.Storage.Model;
using System.ComponentModel;
using System;
using EventMaster.Storage;
using System.Collections.Generic;
using System.Linq;

namespace EventMaster.Course
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        public CourseViewModel(CourseModel storageCourse)
        {
            this.storageCourse = storageCourse;
            this.PropertyChanged += CourseViewModel_PropertyChanged;
        }

        private void CourseViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Workspace.RegisterDataChanged();
        }

        private CourseModel storageCourse;

        public string Id => storageCourse.Id;

        public string PeriodeId
        {
            get { return storageCourse.PeriodeId; }
            set
            {
                storageCourse.PeriodeId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PeriodeId"));
            }
        }

        public string Name
        {
            get { return storageCourse.Name; }
            set
            {
                storageCourse.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayName"));
            }
        }

        public string CourseNumber
        {
            get { return storageCourse.CourseNumber; }
            set
            {
                storageCourse.CourseNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CourseNumber"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayName"));
            }
        }
        public string CourseNumber2
        {
            get { return storageCourse.CourseNumber2; }
            set
            {
                storageCourse.CourseNumber2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CourseNumber2"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayName"));
            }
        }
        public string DisplayName => string.Format("{0} - {1}", CourseNumber + (string.IsNullOrWhiteSpace(CourseNumber2) ? "" : "/" + CourseNumber2), Name);

        public string AdditionalInformation
        {
            get { return storageCourse.AdditionalInformation; }
            set
            {
                storageCourse.AdditionalInformation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AdditionalInformation"));
            }
        }

        public string Date
        {
            get { return storageCourse.Date; }
            set
            {
                storageCourse.Date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
            }
        }

        public string Time
        {
            get { return storageCourse.Time; }
            set
            {
                storageCourse.Time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
            }
        }

        public string MeetPoint
        {
            get { return storageCourse.MeetPoint; }
            set
            {
                storageCourse.MeetPoint = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MeetPoint"));
            }
        }

        public string Date2
        {
            get { return storageCourse.Date2; }
            set
            {
                storageCourse.Date2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date2"));
            }
        }

        public string Time2
        {
            get { return storageCourse.Time2; }
            set
            {
                storageCourse.Time2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time2"));
            }
        }

        public string MeetPoint2
        {
            get { return storageCourse.MeetPoint2; }
            set
            {
                storageCourse.MeetPoint2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MeetPoint2"));
            }
        }

        public string Date3
        {
            get { return storageCourse.Date3; }
            set
            {
                storageCourse.Date3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date3"));
            }
        }

        public string Time3
        {
            get { return storageCourse.Time3; }
            set
            {
                storageCourse.Time3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time3"));
            }
        }

        public string MeetPoint3
        {
            get { return storageCourse.MeetPoint3; }
            set
            {
                storageCourse.MeetPoint3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MeetPoint3"));
            }
        }


        public int MaxNumberOfParticipants
        {
            get { return storageCourse.MaxNumberOfParticipants; }
            set
            {
                storageCourse.MaxNumberOfParticipants = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxNumberOfParticipants"));
            }
        }

        public string EmployeeCourseLeaderId
        {
            get { return storageCourse.EmployeeCourseLeaderId; }
            set
            {
                storageCourse.EmployeeCourseLeaderId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EmployeeCourseLeaderId"));
            }
        }

        public string CourseTypeId
        {
            get { return storageCourse.CourseTypeId; }
            set
            {
                storageCourse.CourseTypeId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CourseTypeId"));
            }
        }        

        public List<CourseParticipantListViewModel> Participants
        {
            get
            {
                var participants = Workspace.CurrentData.CourseParticipants.Where(x => x.CourseId == this.Id).ToList();
                var participantCourseMapping = participants.Select(x => new { Participant = x, Person = Workspace.CurrentData.Participants.Where(c => c.Id == x.ParticipantId).FirstOrDefault() }).ToList();
                var displayPreperation = participantCourseMapping.Where(x => x.Person != null).Select(x => new { Participant = x.Participant, Person = x.Person }).ToList();
                return displayPreperation.Select(x => new CourseParticipantListViewModel
                {
                    Vorname = x.Person.Firstname,
                    Nachname = x.Person.Name,
                    Email = x.Person.Email,
                    Telefon = x.Person.Telefon,
                    Anwesenheit = x.Participant.Present.HasValue ? (x.Participant.Present.Value ? "Anwesend" : "Abwesend") : "Offen",
                    Ersatz = x.Participant.IsReplacementCourse ? "Ersatzkurs" : string.Empty,
                    Laufnummer = x.Participant.SequencialNumber,
                    AnmeldungsId = x.Participant.Id,
                }).ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RemoveCourseFromModel()
        {
            Workspace.CurrentData.Courses.Remove(storageCourse);
            Workspace.RegisterDataChanged();
        }
    }
}