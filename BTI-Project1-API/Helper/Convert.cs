using BTI_Project1_API.Context;
using BTI_Project1_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Helper
{
    public static class Convert
    {
        public static async Task<_Person> DbToPersonAsync(Person person, ApplicationDbContext context)
        {
            _Person convertedPerson = new _Person();

            #region Same property Content Cloning
            //convertedPerson.Id = person.Id;
            //convertedPerson.Name = person.Name;
            //convertedPerson.Surname = person.Surname;
            //convertedPerson.Role = person.Role;
            //convertedPerson.GithubLink = person.GithubLink;
            //convertedPerson.LinkedInLink = person.LinkedInLink;

            foreach (var property in typeof(Person).GetProperties())
            {
                if(property.Name.ToString() == "ProjectIds") continue;
                var editedProp = property.GetValue(person);
                property.SetValue(person, editedProp);
            }
            #endregion

            #region Ids to Project Arrays
            List<Guid> ids = new List<Guid>();

            foreach (var id in person.ProjectIds.Split('-'))
            {
                ids.Add(Guid.Parse(id));
            }

            List<Project> projects = new List<Project>();

            foreach (var id in ids)
            {
                projects.Add(await context.Project.FindAsync(id));
            }

            convertedPerson.Projects = projects.ToArray();
            #endregion

            return convertedPerson;
        }

        public static async Task<_Project> DbToProjectAsync(Project project, ApplicationDbContext context)
        {
            _Project convertedProject = new _Project();

            #region Same property Content Cloning
            convertedProject.Id = project.Id;
            convertedProject.Name = project.Name;
            convertedProject.Explanation = project.Explanation;
            convertedProject.GithubLink = project.GithubLink;

            foreach (var property in typeof(Project).GetProperties())
            {
                if (property.Name.ToString() == "PersonIds") continue;
                var editedProp = property.GetValue(project);
                property.SetValue(project, editedProp);
            }
            #endregion

            #region Ids to Project Arrays
            List<Guid> ids = new List<Guid>();

            foreach (var id in project.PersonIds.Split('-'))
            {
                ids.Add(Guid.Parse(id));
            }

            List<Person> People = new List<Person>();

            foreach (var id in ids)
            {
                People.Add(await context.Person.FindAsync(id));
            }

            convertedProject.People = People.ToArray();
            #endregion

            return convertedProject;
        }
    }
}