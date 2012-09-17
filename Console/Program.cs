﻿using Biz;
using Biz.Models;
using Biz.Managers;
using Biz.Services;
using Biz.Manager;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IDownloadManager ds = new DownloadManager();
            IResourcePackageManager rpm;

            DefaultConstants dc = new DefaultConstants();
            dc.CultureCode = "en";
            dc.SiteVersion = "development";
            dc.PartnerCode = "none";
            dc.LocalContentPath = @"d:\offline\content\";
            dc.LocalMediaPath = @"d:\offline\media\";
            dc.ServicePrefix = "http://mobiledev.englishtown.com";
            dc.ResourcePrefix = "http://local.englishtown.com";

            ICourseStructureManager cs = new CourseStructureManager(ds, 201, dc);
            Course course = cs.BuildCourseStructure();

            // Get all Activities under the level.
            foreach (Level level in course.Levels)
            {
                foreach (Unit unit in level.Units)
                {
                    foreach (Lesson lesson in unit.Lessons)
                    {
                        foreach (Step step in lesson.Steps)
                        {
                            foreach (Activity activity in step.Activities)
                            {
                                IContentDownloadManager activityContent = new ActivityContentDownloadManager(ds, activity, dc);
                            }
                        }

                        IResourcePackageManager mpm = new MediaResourcePackageManager(lesson, dc);
                        mpm.Package();
                    }

                    // Get Unit content structure
                    IContentDownloadManager unitContent = new UnitContentDownloadManager(ds, unit, dc);
                    unitContent.Download();
                }

                // Get level content structure
                IContentDownloadManager levelContent = new UnitContentDownloadManager(ds, level, dc);
                levelContent.Download();

                IResourcePackageManager cpm = new ContentResourcePackageManager(level, dc);
                cpm.Package();
            }

        }
    }
}
