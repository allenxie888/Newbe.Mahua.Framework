﻿using Newbe.Mahua.Messages;
using Newbe.Mahua.Messages.Builders;
using Newbe.Mahua.Messages.Steps;

namespace Newbe.Mahua.QQLight.Messages
{
    public class DiscussMessageStep2 : IDiscussMessageStep2
    {
        private readonly IDiscussMessageDone _discussMessageDone;
        private readonly IAtAll _atAll;
        private readonly IAt _at;
        private readonly IText _text;
        private readonly IImage _image;
        private readonly IFace _face;
        private readonly IBFace _bFace;
        private readonly ISFace _sFace;
        private readonly IEmoji _emoji;

        public DiscussMessageStep2(
            IDiscussMessageDone discussMessageDone,
            IAtAll atAll,
            IAt at,
            IText text,
            IImage image,
            IFace face,
            IBFace bFace,
            ISFace sFace,
            IEmoji emoji)
        {
            _discussMessageDone = discussMessageDone;
            _atAll = atAll;
            _at = at;
            _text = text;
            _image = image;
            _face = face;
            _bFace = bFace;
            _sFace = sFace;
            _emoji = emoji;
        }

        public IDiscussMessageStep2 AtlAll()
        {
            _atAll.AtlAll();
            return this;
        }

        public IDiscussMessageStep2 At(string qq)
        {
            _at.At(qq);
            return this;
        }

        public IDiscussMessageStep2 Text(string text)
        {
            _text.Text(text);
            return this;
        }

        public IDiscussMessageStep2 Newline()
        {
            _text.Newline();
            return this;
        }

        public IDiscussMessageStep2 Image(string file)
        {
            _image.Image(file);
            return this;
        }

        public IDiscussMessageStep2 Face(string id)
        {
            _face.Face(id);
            return this;
        }

        public IDiscussMessageStep2 BFace(string id)
        {
            _bFace.BFace(id);
            return this;
        }

        public IDiscussMessageStep2 SFace(string id)
        {
            _sFace.SFace(id);
            return this;
        }

        public IDiscussMessageStep2 Emoji(string id)
        {
            _emoji.Emoji(id);
            return this;
        }

        public void Done()
        {
            _discussMessageDone.Done();
        }
    }
}
