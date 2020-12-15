# Introduction:

This is a simple web app for user to select one of the provided survey and answers from users. There are three
pages:

1. Dashboard: list available survey(s) and saved user answer(s).
2. Survey detail page: for user to fill the survey.
3. Answer detail page: show details of answers created by users.

There's also a REST-ful Web API for exposing more information. i.e. adding new surveys, update surveys, and
create/update options of answers.

## For running the project:

1. checkout the repo, or un-compress the zip file.
2. use terminal or IDE to open projects.
3. For BE, First need to maks sure GenerateDocument turn on
   ```XML
    <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
      <DocumentationFile>bin\$(Configuration)\SurveyApi.xml</DocumentationFile>
    </PropertyGroup>
   ```
4. Then run in CLI:
   ```
   dotnet restore
   dotnet run --project=SurveyApi 
   -- Then go to https://localhost:5001/swagger/index.html
   ```
5. For FE, run:
   ```
   npm install
   ng serve
   -- Then go to http://localhost:4200/
   ```

## Design:

BE project is a `.net 5` web api with `ef core in-memory db` for poc purpose written in C#. FE is pure `Angular` FE using web api to
communicate with BE.

Both of them could be deployed separately to dockerized management environment, i.e. esc. Sample dockerfiles have
provided.

### Assumption:

1. Only single-select question allow for FE. BE has designed in handling multiple choice and text field questions.
2. Data model design already consider unlimited levels of options. Only need to handle this structure in FE. i.e. based on some selection, the upcoming questions' options could be limited. 

```json
{
  "survey": {
    "questions": [
      {
        "questionId": 1,
        "parentQuestionId": null,
        "options": [
          {
            "optionId": 2,
            "parentOptionId": null,
            "subOptions": [
              {
                "optionId": 3,
                "parentOptionId": 2,
                "subOptions": []
              }
            ]
          }
        ]
      }
    ]
  }
}
```

3. User could create answer and save to db. Answer object design shows below.

```json
{
  "customiseDto": {
    "id": 1,
    "userId": "fake_user_id",
    "surveyId": "SurveyId",
    "createdDateUtc": "2020-12-05",
    "questionAnswers": [
      "option_id_1",
      "option_id_2",
      "option_id_3"
    ]
  }
}
```

## Todo-list:

- [x] Back-end design and first implementation. 2020-12-12
- [x] Back-end test. 2020-12-14
- [x] Front-end scaffolding. 2020-12-14
- [x] Hook up Back-end and Front-end. 2020-12-15
- [x] Front-end UI Kit apply. 2020-12-15
- [x] Final test and clean up. 2020-12-15
- [x] Documentation. 2020-12-15

## Plan to do:

- [ ] Update existing user answer.
- [ ] DB access lock inquiry. (EF already well handled. could be done in architecture level.)
- [ ] FE UI component test.
----------------------------------------------------------------------------------------------

# Requirements:
- [x] Code should be formatted and structured well
- [x] Components should be reusable
- [x] Typesafe code is preferred
- [x] Not locked to any specific technology/language
- [x] A README.md needs to be included so that other developers know how to run your project


# Task:

Create a front-end ​working solution​ based on the background and additional resources listed above and below.
* Create ​documents ​of the solution architecture for the back-end. Include things such as:
  * Technologies to use
  Please refer to above description.
  * API Design
  Please refer to `swagger` api documents.
  * SQL Design
  EF Code-first DB access. queries are written in linq for easy-reading.
  * Tests Design
  BE Service level logic unit test added.
  * Directory structure
  * Diagrams
