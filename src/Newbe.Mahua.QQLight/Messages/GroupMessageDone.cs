﻿using Newbe.Mahua.Messages.Steps;

namespace Newbe.Mahua.QQLight.Messages
{
    public class GroupMessageDone : IGroupMessageDone
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly IQqLightMessage _message;

        public GroupMessageDone(
            IMahuaApi mahuaApi,
            IQqLightMessage message)
        {
            _mahuaApi = mahuaApi;
            _message = message;
        }

        public void Done()
        {
            _mahuaApi.SendGroupMessage(_message.Target, _message.GetMessage());
        }
    }
}
