@echo off
call env.bat

echo Running Functional Tests

%FUNC_RUNNER% %FUNC_SCRIPT_DIR%\success.xml > %FUNC_TEST_LOG%

%FUNC_RUNNER% %FUNC_SCRIPT_DIR%\failure.xml >> %FUNC_TEST_LOG%

%FUNC_RUNNER% %FUNC_SCRIPT_DIR%\skill_list.xml >> %FUNC_TEST_LOG%

%FUNC_RUNNER% %FUNC_SCRIPT_DIR%\nodup_assess_prob.xml >> %FUNC_TEST_LOG%

%FUNC_RUNNER% %FUNC_SCRIPT_DIR%\random_assess_prob.xml >> %FUNC_TEST_LOG%

%FUNC_RUNNER% %FUNC_SCRIPT_DIR%\order_assets_present_tutor.xml >> %FUNC_TEST_LOG%

