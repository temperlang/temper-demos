# Distributing form validation logic

Business logic such as data validation can be useful to share across different
nodes and programming languages, both in client and server.

This demo has a small validation framework with example usage. Here are the
subdirs:

- client-js/ - A JS web client using the Quasar framework
- server-csharp/ - A C# web server using ASP.NET Core minimal REST API
- server-python/ - A Python web server using FastAPI
- temper/ - The Temper validation framework and rules that get used in the other
  languages here
