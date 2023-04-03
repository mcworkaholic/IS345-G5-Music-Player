using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Music_Player
{
    internal class FileSystemTreeNode
    {
        //private static TreeNode _cachedTree;
        private readonly List<FileSystemTreeNode> _children = new List<FileSystemTreeNode>();

        public string DisplayName { get; set; } // Displays the song title w/out file extension and track number
        public string ObjectType { get; set; } // Displays the object type, i.e (artist), (album), (song)
        public string FullPath { get; set; } //  property to store file/folder path
        public string FileName { get; set; } // property to store the name of the song file, i.e "07. A Street I Know.mp3"
        public NodeType NodeType { get; set; } // whether node is a file or folder
        public FileSystemTreeNode Parent { get; set; }
        public IEnumerable<FileSystemTreeNode> Children => _children;

        // Needs to be called from a "song" node
        public string GetAlbumArtPath()
        {
            if (this.Parent == null)
            {
                return null;
            }

            foreach (var childNode in this.Parent.Children)
            {
                if (childNode.DisplayName == "Album Art" && childNode.NodeType == NodeType.Folder)
                {
                    return childNode.FullPath;
                }
            }

            return this.Parent.GetAlbumArtPath();
        }
        //public FileSystemTreeNode FindNodeByDisplayName(string displayName)
        //{
        //    if (this.DisplayName == displayName)
        //    {
        //        return this;
        //    }
        //    foreach (var child in Children)
        //    {
        //        var foundNode = child.FindNodeByDisplayName(displayName);
        //        if (foundNode != null)
        //        {
        //            return foundNode;
        //        }
        //    }
        //    return null;
        //}
        //public FileSystemTreeNode FindNodeByFileName(string fileName)
        //{
        //    // retrieves corresponding node by its filename 
        //    if (this.FileName == fileName)
        //    {
        //        return this;
        //    }
        //    foreach (var child in Children)
        //    {
        //        var foundNode = child.FindNodeByFileName(fileName);
        //        if (foundNode != null)
        //        {
        //            return foundNode;
        //        }
        //    }
        //    return null;
        //}

        public FileSystemTreeNode FindNodeByFullPath(string path)
        {
            if (this.FullPath == path)
            {
                return this;
            }
            foreach (var child in Children)
            {
                var foundNode = child.FindNodeByFullPath(path);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }
        public IEnumerable<FileSystemTreeNode> GetAllNodesExceptRoot()
        {
            var allNodes = new List<FileSystemTreeNode>();
            foreach (var childNode in Children)
            {
                allNodes.Add(childNode);
                allNodes.AddRange(childNode.GetAllNodesExceptRoot());
            }
            return allNodes;
        }
        public void AddChild(FileSystemTreeNode child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public void DeleteChild(FileSystemTreeNode child)
        {
            _children.Remove(child);
        }

        public static FileSystemTreeNode BuildTree(string directoryPath)
        {
            var rootNode = new FileSystemTreeNode
            {
                FullPath = directoryPath,
                FileName = Path.GetFileName(directoryPath),
                DisplayName = Path.GetFileName(directoryPath),
                NodeType = NodeType.Folder,
                ObjectType = "(Music)"
            };

            foreach (var subDirectoryPath in Directory.GetDirectories(directoryPath))
            {
                var artistNode = new FileSystemTreeNode
                {
                    FullPath = subDirectoryPath,
                    FileName = Path.GetFileName(subDirectoryPath),
                    DisplayName = Path.GetFileName(subDirectoryPath),
                    NodeType = NodeType.Folder,
                    ObjectType = "(Artist)"
                };
                rootNode.AddChild(artistNode);

                foreach (var albumDirectoryPath in Directory.GetDirectories(subDirectoryPath))
                {
                    var albumNode = new FileSystemTreeNode
                    {
                        FullPath = albumDirectoryPath,
                        FileName = Path.GetFileName(albumDirectoryPath),
                        DisplayName = Path.GetFileName(albumDirectoryPath),
                        NodeType = NodeType.Folder,
                        ObjectType = "(Album)"
                    };
                    artistNode.AddChild(albumNode);

                    foreach (var filePath in Directory.GetFiles(albumDirectoryPath))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        int firstCharIndex = fileName.IndexOf("-");
                        string title;
                        if (firstCharIndex == -1)
                        {
                            if (fileName.IndexOf('.') != -1)
                            {
                                title = fileName.Substring(fileName.IndexOf('.') + 1).Trim();
                            }
                            else
                            {
                                title = Regex.Replace(fileName, @"^\d+\s*", "").Trim();
                            }
                        }
                        else
                        {
                            string[] parts = fileName.Split('-');
                            title = parts[1].Trim();
                        }
                        var node = new FileSystemTreeNode
                        {
                            FullPath = filePath,
                            FileName = Path.GetFileName(filePath),
                            DisplayName = title,
                            NodeType = NodeType.File,
                            ObjectType = "(Song)"
                        };
                        albumNode.AddChild(node);
                    }
                }
            }
            return rootNode;
        }
    }
    public enum NodeType
    {
        File,
        Folder
    }
}

