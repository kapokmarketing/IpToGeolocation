// SPDX-FileCopyrightText: (c) 2021 Kapok Marketing, Inc.
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.Json.Serialization;

namespace Kapok.IpToGeolocation.IpStack
{
    public enum ErrorType
    {
        // TODO: Can't have numbers in enums. Find solution.
        // 404_not_found,
        missing_access_key,
        invalid_access_key,
        inactive_user,
        invalid_api_function,
        usage_limit_reached,
        function_access_restricted,
        https_access_restricted,
        invalid_fields,
        too_many_ips,
        batch_not_supported_on_plan
    }   
}