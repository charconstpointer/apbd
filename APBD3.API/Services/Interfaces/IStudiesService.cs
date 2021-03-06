﻿using System;
using System.Threading.Tasks;
using APBD3.API.Models;

namespace APBD3.API.Services.Interfaces
{
    public interface IStudiesService
    {
        Task<Enrollment> EnrollStudent(string indexNumber, string firstName, string lastName, DateTime birthDate, string studies);
        Task<Enrollment> PromoteStudents(string studies, int semester);
    }
}