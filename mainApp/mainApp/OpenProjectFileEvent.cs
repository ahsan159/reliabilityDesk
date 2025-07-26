using mainApp.Template;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainApp
{
    public class OpenProjectFileEvent : PubSubEvent<string>
    {

    }
    public class CreateNodeEvent : PubSubEvent<ReliabilityEntity>
    {

    }
}
