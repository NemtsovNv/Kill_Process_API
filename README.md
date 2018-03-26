# Kill_Process_API

## General description
This project is an API solution for process stop test task.

## Quick start
To start project you will need:
1. Visual Studio 2017;
2. .Net Core package installed on machine.

Start process steps:
1. Open solution in Visual Studio;
2. Choose 'KillProcess.API' as startup project;
3. Launch application using F5.

Unit tests launch process:
1. Open solution in Visual Studio;
2. Build application using 'Ctrl+Shift+B' or using 'Build' tab;
3. Open 'Test Explorer';
4. Click on 'Run All' to run all tests.

## Structure overview
Base project structure:

1. KillProcess.API - Web project containing controllers;

2. KillProcess.Domain.Model - Class library, containing models used in solution;

3. KillProcess.Infrastructure.Business - Class library, containing business services;

4. KillProcess.Tests.Unit - Project unit tests.