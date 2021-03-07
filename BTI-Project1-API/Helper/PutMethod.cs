using BTI_Project1_API.Context;
using BTI_Project1_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Helper
{
    public static class PutMethod
    {
        public static async void Person(ApplicationDbContext context, Person person)
        {
            var oldPerson = await context.Person.FindAsync(person.Id);

            if (oldPerson.ProjectIds.Equals(person.ProjectIds))
                return;

            List<string> removed = new List<string>();
            List<string> added = new List<string>();

            foreach (var projectid in oldPerson.ProjectIds.Split('-'))
            {
                if (!projectid.Contains(person.ProjectIds))
                    removed.Add(projectid);
            }

            foreach (var projectid in person.ProjectIds.Split('-'))
            {
                if (!projectid.Contains(oldPerson.ProjectIds))
                    added.Add(projectid);
            }

            foreach (var project in context.Project)
            {
                try
                {
                    if (removed.Contains(project.Id.ToString()))
                    {
                        List<string> personIds = project.PersonIds.Split('-').ToList();
                        personIds.Remove(person.Id.ToString());
                        project.PersonIds = personIds.Count == 1 ? personIds[0] : String.Join('-', personIds);
                    }
                }catch(Exception) { }

                try
                {
                    if (added.Contains(project.Id.ToString()))
                    {
                        List<string> personIds = project.PersonIds.Split('-').ToList();
                        personIds.Add(person.Id.ToString());
                        personIds.Sort();
                        project.PersonIds = personIds.Count == 1 ? personIds[0] : String.Join('-', personIds);
                    }
                }catch(Exception) { }
            }
        }

        public static async void Project(ApplicationDbContext context, Project project)
        {
            var oldProject = await context.Project.FindAsync(project.Id);

            if (oldProject.PersonIds.Equals(project.PersonIds))
                return;

            List<string> removed = new List<string>();
            List<string> added = new List<string>();

            foreach (var personid in oldProject.PersonIds.Split('-'))
            {
                if (!personid.Contains(project.PersonIds))
                    removed.Add(personid);
            }

            foreach (var personid in project.PersonIds.Split('-'))
            {
                if (!personid.Contains(oldProject.PersonIds))
                    added.Add(personid);
            }

            foreach (var person in context.Person)
            {
                try
                {
                    if (removed.Contains(person.Id.ToString()))
                    {
                        List<string> projectIds = person.ProjectIds.Split('-').ToList();
                        projectIds.Remove(project.Id.ToString());
                        person.ProjectIds = projectIds.Count == 1 ? projectIds[0] : String.Join('-', projectIds);
                    }
                }catch(Exception) { }

                try
                {
                    if (added.Contains(person.Id.ToString()))
                    {
                        List<string> projectIds = person.ProjectIds.Split('-').ToList();
                        projectIds.Add(project.Id.ToString());
                        projectIds.Sort();
                        person.ProjectIds = String.Join('-', projectIds);
                    }
                }catch(Exception) { }
            }
        }

        
    }
}
