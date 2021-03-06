﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<TestLeaflet.Models.ExternalLogin>>" %>

<% if (Model.Count > 0) { %>
    <h3>Зарегистрированные внешние учетные записи</h3>
    <table>
        <tbody>
        <% foreach (TestLeaflet.Models.ExternalLogin externalLogin in Model) { %>
            <tr>
                <td><%: externalLogin.ProviderDisplayName %></td>
                <td>
                    <% if (ViewBag.ShowRemoveButton) {
                        using (Html.BeginForm("Disassociate", "Account")) { %>
                            <%: Html.AntiForgeryToken() %>
                            <fieldset>
                                <%: Html.Hidden("provider", externalLogin.Provider) %>
                                <%: Html.Hidden("providerUserId", externalLogin.ProviderUserId) %>
                                <input type="submit" value="Удалить" title="Удалите эти <%: externalLogin.ProviderDisplayName %> учетные данные из своей учетной записи" />
                            </fieldset>
                        <% }
                    } else { %>
                        &nbsp;
                    <% } %>
                </td>
            </tr>
        <% } %>
        </tbody>
    </table>
<% } %>
