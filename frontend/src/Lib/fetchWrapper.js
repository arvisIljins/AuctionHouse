import { handleGetToken } from "@/app/services/authService";

const baseUrl = "http://localhost:6001/";

async function get(url) {
  const request = {
    method: "GET",
    headers: await getHeader(),
  };

  const response = await fetch(`${baseUrl}${url}`, request);
  return await handleResponse(response);
}

async function post(url, body) {
  const request = {
    method: "POST",
    headers: await getHeader(),
    body: JSON.stringify(body),
  };

  const response = await fetch(`${baseUrl}${url}`, request);
  return await handleResponse(response);
}

async function put(url, body) {
  const request = {
    method: "PUT",
    headers: await getHeader(),
    body: JSON.stringify(body),
  };

  const response = await fetch(`${baseUrl}${url}`, request);
  return await handleResponse(response);
}

async function del(url) {
  const request = {
    method: "DELETE",
    headers: await getHeader(),
  };

  const response = await fetch(`${baseUrl}${url}`, request);
  return await handleResponse(response);
}

async function getHeader() {
  const token = await handleGetToken();
  const headers = {
    "Content-type": "application/json",
  };

  if (token) {
    headers.Authorization = `Bearer ${token.access_token}`;
  }
  return headers;
}

async function handleResponse(response) {
  const text = await response.text();
  const data = text && JSON.parse(text);
  if (response.ok && data.success) {
    return data;
  } else {
    return { success: false, message: data.message };
  }
}

export const fetchWrapper = {
  get,
  post,
  put,
  del,
};
