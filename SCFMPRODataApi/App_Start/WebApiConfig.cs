
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.Cors;
using System.Web.Mvc.Routing;
using PsiMprODataApi.Models;
using PsiMprODataApi.App_Start;

namespace PsiMprODataApi
{
    public static class WebApiConfig
    {


        public static HttpConfiguration Register(HttpConfiguration config)
        { 
            config.MapHttpAttributeRoutes();

            
              ODataConventionModelBuilder builder =  new ODataConventionModelBuilder();
              builder.EntitySet<mdProject>("mdProjects");
              builder.EntitySet<AspNetUser>("AspNetUsers");
              builder.EntitySet<mdContractPrimaryDeliverable>("mdContractPrimaryDeliverables");
              builder.EntitySet<mdContractSecondaryDeliverable>("mdContractSecondaryDeliverables");
              builder.EntitySet<mdCustomerDeliverableVoltage>("mdCustomerDeliverableVoltages");

               builder.EntitySet<mdInternalStakeHolder>("mdInternalStakeHolders");


                builder.EntitySet<mdExternalStakeHolder>("mdExternalStakeHolders"); 
              builder.EntitySet<mdMasterProjectSchedule>("mdMasterProjectSchedules");
              builder.EntitySet<msCompany>("msCompanies");
              builder.EntitySet<msSubstationStructure>("msSubstationStructures");
              builder.EntitySet<mdProjectStakeHolder>("mdProjectStakeHolders");
              builder.EntitySet<mdRevisionHistory>("mdRevisionHistories");
              builder.EntitySet<mdSubstation>("mdSubstations");
              builder.EntitySet<msRequiredQuantity>("msRequiredQuantities");
              builder.EntitySet<msConsumable>("msConsumables");
              builder.EntitySet<msGenSchemeComponent>("msGenSchemeComponents"); 

              builder.EntitySet<msVtWindingFunciton>("msVtWindingFuncitons");
              builder.EntitySet<msVoltageLevel>("msVoltageLevels");
              builder.EntitySet<msTNBApprovedEquipment>("msTNBApprovedEquipment");
              builder.EntitySet<msSubstationType>("msSubstationType");

              builder.EntitySet<msSubstationObjectType>("msSubstationObjectType");
              builder.EntitySet<msScheduleHItem>("msScheduleHItem"); 
              builder.EntitySet<msRelayFunction>("msRelayFunction"); 
              builder.EntitySet<msPsiAtt>("msPsiAtt");
              builder.EntitySet<msProtectionFunction>("msProtectionFunction");
              builder.EntitySet<msPrimaryEquipment>("msPrimaryEquipments");
              builder.EntitySet<msPrimaryEquipmentDrawing>("msPrimaryEquipmentDrawing");
              builder.EntitySet<msPanelType>("msPanelType");
              builder.EntitySet<msPanel>("msPanel");
              builder.EntitySet<msPanelArgType>("msPanelArgType");
              builder.EntitySet<msMPRSetting>("msMPRSetting");
              builder.EntitySet<msMimic>("msMimic");
              builder.EntitySet<msLogoType>("msLogoType");
              builder.EntitySet<msLocationDesignation>("msLocationDesignation");
              builder.EntitySet<msInsulationType>("msInsulationType");
              builder.EntitySet<msInitial>("msInitial");
              builder.EntitySet<msDrawingType>("msDrawingTypes");
              builder.EntitySet<msDrawing>("msDrawings");
              builder.EntitySet<msDrawingFunction>("msDrawingFunctions");
              builder.EntitySet<msCustomerDeliverable>("msCustomerDeliverables");
              builder.EntitySet<msCtCoreFunction>("msCtCoreFunctions");
              builder.EntitySet<msControlFunction>("msControlFunction");
              builder.EntitySet<msContactDetail>("msContactDetail");

              builder.EntitySet<msComponentCategory>("msComponentCategory");
              builder.EntitySet<msCommunicationType>("msCommunicationType");
              builder.EntitySet<msBusConfiguration>("msBusConfiguration");
              builder.EntitySet<msBayType>("msBayType");
              builder.EntitySet<msBayRelayFunction>("msBayRelayFunction");
              builder.EntitySet<mdWinding>("mdWinding");
              builder.EntitySet<mdVPanel>("mdVPanel");
              builder.EntitySet<mdVoltageTransformer>("mdVoltageTransformer");
              builder.EntitySet<mdVoltageLevel>("mdVoltageLevels");
              builder.EntitySet<mdTransducer>("mdTransducer");
              builder.EntitySet<mdTransducerChannelSpec>("mdTransducerChannelSpec");
              builder.EntitySet<mdTestBlock>("mdTestBlock");
              builder.EntitySet<mdTerminalBlock>("mdTerminalBlock");

              builder.EntitySet<mdSPanel>("mdSPanel");
              builder.EntitySet<mdSingleLogo>("mdSingleLogo");
              builder.EntitySet<mdSchematicProject>("mdSchematicProject");
              builder.EntitySet<mdSchematicProjectPath>("mdSchematicProjectPath");

              builder.EntitySet<mdRemoteEndDetail>("mdRemoteEndDetail");

              builder.EntitySet<mdProjectLogo>("mdProjectLogo");

              builder.EntitySet<mdPRData>("mdPRData");
              builder.EntitySet<mdPowerTransformer>("mdPowerTransformer");
              builder.EntitySet<mdPanelsSub>("mdPanelsSub");
              builder.EntitySet<mdPanelSizesSub>("mdPanelSizesSub");
              builder.EntitySet<mdPanelSize>("mdPanelSize");
              builder.EntitySet<mdPanel>("mdPanel");
              builder.EntitySet<mdPanelFunctionsSub>("mdPanelFunctionsSub");
              builder.EntitySet<mdPanelFunction>("mdPanelFunction");
              builder.EntitySet<mdPanelComponent>("mdPanelComponent");
              builder.EntitySet<mdMeter>("mdMeter");

              builder.EntitySet<mdMainRelay>("mdMainRelay");
              builder.EntitySet<mdLogoDetail>("mdLogoDetail");
              builder.EntitySet<mdIsolator>("mdIsolator");

              builder.EntitySet<mdFileTime>("mdFileTime");
              builder.EntitySet<mdEarthSwitch>("mdEarthSwitch");
              builder.EntitySet<mdDoubleLogo>("mdDoubleLogo");

              builder.EntitySet<mdCustomerDeliverableDate>("mdCustomerDeliverableDate");
              builder.EntitySet<mdCurrentTransformer>("mdCurrentTransformer");
              builder.EntitySet<mdCore>("mdCore");


              builder.EntitySet<mdContactDetail>("mdContactDetail");
              builder.EntitySet<mdConsumableItem>("mdConsumableItem");
              builder.EntitySet<mdCircuitBreaker>("mdCircuitBreaker");


              builder.EntitySet<mdAuxiliaryRelay>("mdAuxiliaryRelay");
              builder.EntitySet<mdAuxiliaryComponent>("mdAuxiliaryComponent");
              builder.EntitySet<mdArrangementSynchCircuitRow>("mdArrangementSynchCircuitRows");
              builder.EntitySet<mdArrangementsIndicationRow>("mdArrangementsIndicationRows");
              builder.EntitySet<mdArrangementPanelRow>("mdArrangementPanelRows");
              builder.EntitySet<mdArrangementAnnunciationRow>("mdArrangementAnnunciationRows");
              builder.EntitySet<mdArrangementAcLoopRow>("mdArrangementAcLoopRows");
              builder.EntitySet<mdAnnunciator>("mdAnnunciator");
              builder.EntitySet<mdAllTerminalBlocksFromScheme>("mdAllTerminalBlocksFromScheme");
              builder.EntitySet<mdAllComponentsFromScheme>("mdAllComponentsFromScheme");

              builder.EntitySet<AspNetUserLogin>("AspNetUserLogin");
              builder.EntitySet<AspNetUserClaim>("AspNetUserClaim");
              builder.EntitySet<AspNetRole>("AspNetRole");
              builder.EntitySet<AspNetRoleClaim>("AspNetRoleClaim");
              builder.EntitySet<msStakeHolderContact>("msStakeHolderContacts");
             
              builder.EntitySet<mdBay>("mdBays");

              builder.EntitySet<mdBPanel>("mdBPanels");
              builder.EntitySet<msSubstationStructure>("msSubstationStructures");
              builder.EntitySet<mdPrimaryEquipment>("mdPrimaryEquipments");



            var conventions = System.Web.Http.OData.Routing.Conventions.ODataRoutingConventions.CreateDefault();
             
            conventions.Insert(0, new MatchAllRoutingConvention());
             
             config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel(), new System.Web.Http.OData.Routing.DefaultODataPathHandler(), conventions);   
            //config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());




            return config;
        }

        
    }


     

}
