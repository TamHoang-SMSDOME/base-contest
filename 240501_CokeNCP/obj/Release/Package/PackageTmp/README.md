# Contest: 210928_NESTLE
## Introduction 
This project is a contest system with the following key features.

| Contest | Remark |
| ------ | ------ |
| User Requirements | [User Requirements] |
| Test Date | 10 Aug 2021 |
| Start Date | 21 Aug 2021 |
| End Date | 30 Oct 2021 |
| Azure URL | https://bc-210928-nestle.azurewebsites.net |
| Custom Domain | https://www.nestleluckydraw.com |
| Keyword | NESTLE |
| App ID | 1234 |
| PRD Upload Link | https://tinyurl.com/nestleluckydraw |
| API URL | https://www.nestleluckydraw.com/api/entry/add |

#### Participant User Interfaces
- Contest entry via SMS
- Additional receipt submission webpage for SMS entries
- Contest entry via Whatsapp
- Contest entry via online submission webpage
- Contest entry from client's webpage via API

#### Admin User Interfaces
- View contest entries
- Pick winners
- Winner redemption management

#### Backend Jobs
- Scheduled pick winner every Monday 12 AM SGT

## Getting Started
Refer to Work Instructions for actual details. Below is just a general idea  to get the code up and running. Special instructions like cron job for scheduled pick winner should be highlighted.
1.	Installation process
1.1. Create database tables for contest
1.2. Create/publish web app to Azure App Service
1.3. Configure application settings in Azure App Service
1.4. Publish web job to Azure App Service for scheduled pick winner

```
code blocks for commands
```

2.	Software dependencies
2.1. NIL	

3.	Latest releases

| Release Date | Description |
| ------ | ------ |
| 29 Aug 2021 | Fixed outlet dropdown at online submission page |
| 27 Aug 2021 | Initial release |

4.	API references

| API | Reference |
| ------ | ------ |
| SendGrid | [SendGrid API] |

## Build and Test
TODO: Describe and show how to build your code and run the tests. 

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen.)

   [User Requirements]: <https://smsdome.sharepoint.com/:w:/r/sites/Dev2/Shared%20Documents/General/Shared%20Folders/Shared%20Dev%20-%20Sales/Contest%20Requests/{TEST%20DATE}%20_%20{START%20DATE}~{END%20DATE}%20{KEYWORD}%20({SALES})%20-%20NEW.docx?d=wf86efa04fd664c28a69e0e36f8e6bfd0&csf=1&web=1&e=g8DHyS>
   
   [SendGrid API]: <https://docs.sendgrid.com/for-developers/sending-email/integrating-with-the-smtp-api>
