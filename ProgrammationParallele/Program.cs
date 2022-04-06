// See https://aka.ms/new-console-template for more information
using ProgrammationParallele;

Console.WriteLine("Hello, World!");

var imageTools = new ImageTool();
string name = "364376";
imageTools.resizeImage(imageTools.loadImage(name),new System.Drawing.Size(500,100), name);

