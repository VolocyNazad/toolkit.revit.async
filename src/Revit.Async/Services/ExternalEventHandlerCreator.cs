
using Autodesk.Revit.UI;


namespace Revit.Async.ExternalEvents
{
    internal sealed class ExternalEventHandlerCreator : SyncGenericExternalEventHandler<IExternalEventHandler, ExternalEvent>
    {

        public override string GetName()
        {
            return $"ExternalEventHandlerCreator-{Id}";
        }

        public override object Clone()
        {
            return new ExternalEventHandlerCreator();
        }

        protected override ExternalEvent Handle(UIApplication app, IExternalEventHandler parameter)
        {
            return ExternalEvent.Create(parameter);
        }

    }
}
