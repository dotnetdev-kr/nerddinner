# NerdDinner

**Project Description**

The Open Source ASP.NET MVC Project that helps nerds and computer people plan get-togethers. **You can see the site running LIVE at [http://www.nerddinner.com](http://www.nerddinner.com). **This project's goal is to create the best website for Technology People to host their Lunches, Flashmobs, Dinners and informal get-togethers.   

As of **August 2011,** we are almost done updating the source to ASP.NET MVC 3 and adding a bunch of new features. You can get the source from the [Source Code](http://nerddinner.codeplex.com/SourceControl/list/changesets) tab above if you like, or wait for a formal release in the next few weeks.

Here's what we're working on:

*   Mvc 3 + Razor - added
*   EF Code First (for a database that already exists!) - added
*   All libraries via NuGet - added
*   [YepNope](http://yepnopejs.com/) with [Modernizr](http://www.modernizr.com/) - added
*   [JQuery Mobile](http://jquerymobile.com/) beta 2 - added, some back button issues
*   <span style="color:#ff0000">An updated </span>[<span style="color:#ff0000">**MobileCapableRazorViewEngine**</span>](http://nerddinner.codeplex.com/SourceControl/changeset/view/69349#1511642)<span style="color:#ff0000">fixing a nasty caching bug </span>[<span style="color:#ff0000">Scott introduced a few years back</span>](http://www.hanselman.com/blog/ABetterASPNETMVCMobileDeviceCapabilitiesViewEngine.aspx)
    *   This probably needs to be tested some more then Pete and I will make it a NuGet package for MVC3\. MVC4 will have formal support for mobile views baked in.
*   [Geolocation](http://mourfield.com/2011/08/26/adding-html5-geolocation-to-nerd-dinner-with-yepnope-js-and-modernizr/), both desktop and mobile - added, some mobile work needed
*   [MvcHtml5Templates](http://nuget.org/List/Packages/MvcHtml5Templates) by Scott Kirkland - added
*   [ELMAH](http://code.google.com/p/elmah/) (and [MiniProfiler](https://github.com/SamSaffron/MVC-Mini-Profiler) to come soon) - almost done
*   [51Degrees](http://51degrees.codeplex.com/) Mobile capabilities module - added
*   [DotNetOpenAuth](http://www.dotnetopenauth.net/) by Andrew Arnott - added
*   proper [Web Deploy Transforms - added](http://www.hanselman.com/blog/SlowCheetahWebconfigTransformationSyntaxNowGeneralizedForAnyXMLConfigurationFile.aspx)
*   OpenID - added
*   Support all Countries - added
*   Exploit Virtual Earth's APIs more - added "draggable pins"
*   RSS Feeds for all pages - added a main one, need more?
*   iCal downloads for all events - added an iCal feed, and iCal for each details
*   "My" Dinners - added
*   Mobile version of the site - added
*   Twitter integration? Facebook integration? - Added Facebook, Twitter, sharing 
*   Blog Badges showing nearby dinners with automatic geo-location. - Added Flair, updating with cleaner implementation
*   As many Tests and as much Coverage as we can stomach - need volunteers
*   Continuous Integration Server - almost done. Thanks JetBrains!

We are currently adding lots of new features to NerdDinner. Would you like to help?

**The original tutorial no longer applies exactly, as it was written a few years ago for an older version of MVC.** That said, it can still be a good learning tool so you can download a 185-page free PDF walkthrough loaded with code and screenshots at [http://tinyurl.com/aspnetmvc](http://tinyurl.com/aspnetmvc).  

Also, if you have made a modification to NerdDinner, for example, to get it to run on Azure, or with NHibernate, let us know and we'll link it here!

### Other NerdDinner forks/versions out there:

*   [Extending NerdDinner: Exploring Different Database Options](http://www.hanselman.com/blog/ExtendingNerdDinnerExploringDifferentDatabaseOptions.aspx)
*   [Nerd Dinner on Azure and SQL Azure (2009)](http://www.marcmywords.org/post/NerdDinner-hosted-on-Windows-Azure-and-SQL-Azure.aspx)
*   [NerdBytes on Azure](http://blogs.msdn.com/b/jimoneil/archive/2009/07/27/nerddinner-on-azure-take-2.aspx)
*   Phil Hey has [converted the original version to Visual Basic](http://philhey.co.uk/Article/Details/VB_Net_MVC_NerdDinner_Source_Code)

Leave your feature ideas at [http://feedback.nerddinner.com](http://feedback.nerddinner.com). Put bugs above in the "Issue Tracker."

Here's instructions if you want to run [Nerddinner on Mono/Linux](http://www.jprl.com/Blog/archive/development/mono/2009/May-14.html).
