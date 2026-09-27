---
name: verify-app
description: Build and run the ASP.NET Core app, then hit the affected routes to confirm a change actually works. Use after changing controllers, views, models, or Program.cs.
---

1. Run `dotnet build` from the project root. If it fails, fix the errors before continuing.
2. Start the app in the background: `dotnet run --no-build --launch-profile http` (serves http://localhost:5138).
3. Wait until `curl -s -o /dev/null -w "%{http_code}" http://localhost:5138/` returns 200 (poll, don't sleep blindly).
4. `curl` each route affected by the change (default route pattern is `{controller=Home}/{action=Index}/{id?}`, e.g. `/Home/Privacy`). Check the status code and grep the HTML for the expected content.
5. Stop the background server when done.
6. Report: build result, each route checked with status code, and anything that looked wrong.
