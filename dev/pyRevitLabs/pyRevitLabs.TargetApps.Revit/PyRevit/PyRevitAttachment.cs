﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pyRevitLabs.Common;
using pyRevitLabs.TargetApps.Revit;

namespace pyRevitLabs.TargetApps.Revit {
    public class PyRevitAttachment {
        private PyRevitClone _clone = null;

        public PyRevitAttachment(RevitAddonManifest manifest,
                                 RevitProduct product,
                                 PyRevitAttachmentType attachmentType) {
            Manifest = manifest;
            Product = product;
            AttachmentType = attachmentType;
        }

        public override string ToString() {
            return string.Format(
                "{0} | Product: \"{1}\" | Engine: {2} | Path: \"{3}\" | Manifest: \"{4}\"",
                Clone.Name,
                Product.ProductName,
                Engine.Version,
                Clone.ClonePath,
                Manifest.FilePath
                );
        }

        public RevitAddonManifest Manifest { get; private set; }
        public RevitProduct Product { get; private set; }
        public PyRevitAttachmentType AttachmentType { get; private set; }

        public PyRevitClone Clone {
            get {
                if (_clone != null)
                    return _clone;
                else {
                    try {
                        return PyRevitClone.GetCloneFromManifest(Manifest);
                    }
                    catch {
                        return null;
                    }
                }
            }
        }

        public PyRevitEngine Engine => PyRevitEngine.GetEngineFromManifest(Manifest, Clone);
        public bool AllUsers => AttachmentType == PyRevitAttachmentType.AllUsers;

        public void SetClone(PyRevitClone clone) {
            _clone = clone;
        }
    }
}
