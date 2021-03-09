using BTI_Project1_API.Context;
using BTI_Project1_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTI_Project1_API.Helper
{
    public static class PutMethod
    {
        public static void Person(ApplicationDbContext context, Person person)
        {
            //Person oldPerson = context.Person.Find(person.Id);

            Person oldPerson = new Person();

            foreach (var dbperson in context.Person)
            {
                if (dbperson.Id == person.Id)
                {
                    oldPerson = dbperson;
                    break;
                }
            }

            if (oldPerson == null)
                return;

            if (oldPerson.ProjectIds.Equals(person.ProjectIds))
                return;

            List<string> removed = new List<string>();
            List<string> added = new List<string>();

            foreach (var projectid in oldPerson.ProjectIds.Split('-'))
            {
                if (person.ProjectIds.IndexOf(projectid) == -1)
                    removed.Add(projectid);
            }

            foreach (var projectid in person.ProjectIds.Split('-'))
            {
                if (oldPerson.ProjectIds.IndexOf(projectid) == -1)
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

        public static void Project(ApplicationDbContext context, Project project)
        {
            //var oldProject = await context.Project.FindAsync(project.Id);

            Project oldProject = new Project();

            foreach (var dbproject in context.Project)
            {
                if (dbproject.Id == project.Id)
                {
                    oldProject = dbproject;
                    break;
                }
            }

            if (oldProject == null)
                return;

            if (oldProject.PersonIds.Equals(project.PersonIds))
                return;

            List<string> removed = new List<string>();
            List<string> added = new List<string>();

            try
            {
                foreach (var personid in oldProject.PersonIds.Split('-'))
                {
                    if (project.PersonIds?.IndexOf(personid) == -1)
                        removed.Add(personid);
                }
            }
            catch (NullReferenceException)
            {

            }

            try
            {
                foreach (var personid in project.PersonIds.Split('-'))
                {
                    if (oldProject.PersonIds?.IndexOf(personid) == -1)
                        added.Add(personid);
                }
            }
            catch (NullReferenceException)
            {
                
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
